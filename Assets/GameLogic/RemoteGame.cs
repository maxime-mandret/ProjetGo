using System;
using System.Linq;
using System.Threading;
using Assets.ObjetsDeJeu;
using Assets.Db;
using Debug = System.Diagnostics.Debug;

namespace Assets.GameLogic
{
    public class RemoteGame : Game, IDisposable, IObserver<RemoteMovesStalker>, IObserver<RemotePlayerStalker>
    {
        public DbGobansDataContext ApplicationDataContext { get; set; }

        public DbPartie DbPartie
        {
            get { return _dbPartie; }
            set { _dbPartie = value; }
        }

        public DbJoueur DbWhitePlayer
        {
            get { return _dbWhitePlayer; }
            set { _dbWhitePlayer = value; }
        }

        public DbJoueur DbBlackPlayer
        {
            get { return _dbBlackPlayer; }
            set { _dbBlackPlayer = value; }
        }

        public DbJoueur CurrentDbPlayer { get; set; }

        private EventWaitHandle remotePlayerPlayed = new EventWaitHandle(false, EventResetMode.AutoReset);
        private RemoteMovesStalker moveStalker;
        private RemotePlayerStalker playerStalker;

        private DbPartie _dbPartie;
        private DbJoueur _dbWhitePlayer;
        private DbJoueur _dbBlackPlayer;

        // Creation Partie
        public RemoteGame (int size)
            : base(size, new SladIaPlayer("Duck My Sick", PlayerColor.Black), null)
        {
            this.ApplicationDataContext = new DbGobansDataContext();

            this.DbBlackPlayer = DbJoueur.ConnectOrCreatePlayer(this.BlackPlayer.Name, ApplicationDataContext);
            this.CurrentDbPlayer = DbBlackPlayer;
            this.DbPartie = new DbPartie { DbJoueurs_IdJoueurNoir = this.DbBlackPlayer, DbJoueurs_IdJoueurBlanc =  null, HeureDebut = DateTime.Now };
            DbPartie.EtatPartie = GameStatuts.pendingPlayer.ToString();

            this.ApplicationDataContext.DbParties.InsertOnSubmit(this.DbPartie);

            this.ApplicationDataContext.SubmitChanges();
            this.moveStalker = new RemoteMovesStalker(this);
            this.moveStalker.Observers.Add(this);

            this.playerStalker = new RemotePlayerStalker(this);
            this.playerStalker.Observers.Add(this);
        }

        //Rejoindre en attente
        public RemoteGame (DbPartie partie)
            : base(
            9,
            partie.DbJoueurs_IdJoueurNoir == null ? (Player)new SladIaPlayer("Duck My Sick", PlayerColor.Black) : new RemotePlayer(partie.DbJoueurs_IdJoueurNoir.Nom, PlayerColor.Black),
            partie.DbJoueurs_IdJoueurBlanc == null ? (Player)new SladIaPlayer("Duck My Sick", PlayerColor.White) : new RemotePlayer(partie.DbJoueurs_IdJoueurBlanc.Nom, PlayerColor.White))
        {
            this.ApplicationDataContext = new DbGobansDataContext();
            this.DbPartie = this.ApplicationDataContext.DbParties.First(part => part.IdPartie == partie.IdPartie);
            
            this.DbBlackPlayer = DbJoueur.ConnectOrCreatePlayer(this.BlackPlayer.Name, ApplicationDataContext);
            this.DbWhitePlayer = DbJoueur.ConnectOrCreatePlayer(this.WhitePlayer.Name, ApplicationDataContext);
            this.SynchCurrentPlayer();
            // Récupération des coups
            foreach (DbCoup dbCoup in this.DbPartie.DbCoups.OrderBy(p => p.HeureCoup))
            {
                // Méthode classe mère car on ne sauvegarde pas le mouvement (vu qu'il existe deja)
                if (dbCoup.X.HasValue && dbCoup.Y.HasValue)
                {
                    // On apelle la méhode mere pour le pas inserer en base les coups qui y sont déjà
                    base.PutRock((int)dbCoup.X.Value, (int)dbCoup.Y);
                    this.SynchCurrentPlayer();
                }
                else
                {
                    base.PasserTour();
                }
                
            }
            
            moveStalker = new RemoteMovesStalker(this);
            moveStalker.Observers.Add(this);
        }

        private void SynchCurrentPlayer ()
        {
            this.CurrentDbPlayer = this.CurrentPlayer == this.WhitePlayer ? this.DbWhitePlayer: this.DbBlackPlayer;
        }

        public new void EndGame ()
        {
            base.EndGame();
            this.DbPartie.EtatPartie = "over";
            //using (DbGobansDataContext context = ApplicationDataContext)
            //{
            this.DbPartie.HeureFin = DateTime.Now;
            ApplicationDataContext.SubmitChanges();
            //}
            this.ApplicationDataContext.Dispose();
        }

        public new void PutRock (int x, int y)
        {
            base.PutRock(x, y);
            // Enregistrer en base
            //using (DbGobansDataContext context = this.ApplicationDataContext)
            //{
            this.DbPartie.PoserPion(x, y, this.CurrentDbPlayer);
            ApplicationDataContext.SubmitChanges();

            this.SynchCurrentPlayer();

            
            //}
        }

        public new void PasserTour ()
        {
            base.PasserTour();
            // Enregistrer en base
            //using (DbGobansDataContext context = this.ApplicationDataContext)
            //{
            this.DbPartie.PasserTour(this.CurrentDbPlayer);
            ApplicationDataContext.SubmitChanges();

            this.SynchCurrentPlayer();
            //}
        }

        public new void Update ()
        {
            base.Update();
            
        }

        #region IDisposable Membres

        public void Dispose ()
        {
            this.ApplicationDataContext.Dispose();
        }

        #endregion

        #region IObserver<RemoteMovesStalker> Membres

        public void ObservedNotified (RemoteMovesStalker remoteMovesStalker)
        {
            Debug.WriteLine(string.Format("Stalker timer ticked !"));
            DbCoup lastCoup = moveStalker.LastCoupPlayed;
            // Jouer le coup sur l'interface
            if (lastCoup.X.HasValue && lastCoup.Y.HasValue)
            {
                base.PutRock(lastCoup.X.Value, lastCoup.Y.Value);
                this.SynchCurrentPlayer();
            }
            else
            {
                base.PasserTour();
            }
        }

        #endregion

        #region IObserver<RemotePlayerStalker> Membres

        public void ObservedNotified (RemotePlayerStalker observed)
        {
            this.WhitePlayer = new RemotePlayer(observed.WaitedPlyer.Nom, PlayerColor.White);
            this.DbWhitePlayer = observed.WaitedPlyer;
            this.Status = GameStatuts.playing;
        }

        #endregion
    }
}
