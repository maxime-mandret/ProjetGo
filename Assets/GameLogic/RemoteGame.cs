using System;
using System.Linq;
using System.Threading;
using Assets.Db;
using Assets.GameUtils;
using Assets.GameUtils.Sgf;
using Assets.ObjetsDeJeu;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Assets.GameLogic
{
    public class RemoteGame : Game, IDisposable, IObserver<RemoteMovesStalker>
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

        private DbPartie _dbPartie;
        private DbJoueur _dbWhitePlayer;
        private DbJoueur _dbBlackPlayer;

        // Creation Partie
        public RemoteGame (int size, Player blackPlayer, Player whitePlayer = null)
            : base(size, blackPlayer, whitePlayer)
        {
            this.ApplicationDataContext = new DbGobansDataContext();

            this.DbBlackPlayer = DbJoueur.ConnectOrCreatePlayer(blackPlayer.Name, ApplicationDataContext);
            this.CurrentDbPlayer = DbBlackPlayer;
            this.DbPartie = new DbPartie { DbJoueurs_IdJoueurNoir = this.DbBlackPlayer, HeureDebut = DateTime.Now };
            // Création en base
            if (whitePlayer != null)
            {
                this.DbWhitePlayer = DbJoueur.ConnectOrCreatePlayer(whitePlayer.Name, ApplicationDataContext);
                this.DbPartie.DbJoueurs_IdJoueurBlanc = this.DbWhitePlayer;
                //this.Status = "playing";
                DbPartie.EtatPartie = "playing";
            }
            else
            {
                this.Status = "pending";
                DbPartie.EtatPartie = "pending";
            }

            this.ApplicationDataContext.DbParties.InsertOnSubmit(this.DbPartie);

            this.ApplicationDataContext.SubmitChanges();
            this.moveStalker = new RemoteMovesStalker(this);
            this.moveStalker.Observers.Add(this);
        }

        //Rejoindre déjà commencée
        public RemoteGame (DbPartie partie)
            : base(9, ToLocalPlayerModel(partie.DbJoueurs_IdJoueurBlanc, PlayerColor.White), ToLocalPlayerModel(partie.DbJoueurs_IdJoueurNoir, PlayerColor.Black))
        {
            this.ApplicationDataContext = new DbGobansDataContext();
            this.DbPartie = partie;
            this.DbBlackPlayer = partie.DbJoueurs_IdJoueurNoir;
            this.DbWhitePlayer = partie.DbJoueurs_IdJoueurBlanc;

            // Récupération des coups
            foreach (DbCoup dbCoup in this.DbPartie.DbCoups.OrderBy(p => p.HeureCoup))
            {
                // Méthode classe mère car on ne sauvegarde pas le mouvement (vu qu'il existe deja)
                if (dbCoup.X.HasValue && dbCoup.Y.HasValue)
                {
                    //var player = dbCoup.IdJoueur == dbCoup.DbPartie.IdJoueurBlanc
                    //    ? this.WhitePlayer
                    //    : this.BlackPlayer;

                    //this.UIManager.PoserPion(player, (int)dbCoup.X.Value, (int)dbCoup.Y);
                    base.PutRock((int)dbCoup.X.Value, (int)dbCoup.Y);
                }
                else
                {
                    base.PasserTour();
                }
                
            }
            
            moveStalker = new RemoteMovesStalker(this);
            moveStalker.Observers.Add(this);
        }

        private static Player ToLocalPlayerModel (DbJoueur dbJoueur, PlayerColor playerColor)
        {
            return new Player(dbJoueur.Nom, playerColor);
        }

        public new void EndGame ()
        {
            base.EndGame();
            this.DbPartie.EtatPartie = "over";
            //using (DbGobansDataContext context = ApplicationDataContext)
            //{
            this.DbPartie.EtatPartie = "over";
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

            this.CurrentDbPlayer = CurrentDbPlayer == DbBlackPlayer ? DbWhitePlayer : DbBlackPlayer;

            
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

            this.CurrentDbPlayer = CurrentDbPlayer == DbBlackPlayer ? DbWhitePlayer : DbBlackPlayer;
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
                base.PutRock((int)lastCoup.X.Value, (int)lastCoup.Y);
                this.CurrentDbPlayer = CurrentDbPlayer == DbBlackPlayer ? DbWhitePlayer : DbBlackPlayer;
            }
            else
            {
                base.PasserTour();
            }
        }

        #endregion

        
    }
}
