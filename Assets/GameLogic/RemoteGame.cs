using System;
using System.Linq;
using System.Threading;
using Assets.GameUtils;
using Assets.GameUtils.Sgf;
using Assets.ObjetsDeJeu;
using DbGobansContext;
using UnityEngine;

namespace Assets.GameLogic
{
    public class RemoteGame : Game, IDisposable, IObserver<RemoteMovesStalker>
    {
        public DbGobansDataContext ApplicationDataContext { get; set; }
        private EventWaitHandle remotePlayerPlayed = new EventWaitHandle(false, EventResetMode.AutoReset);
        private RemoteMovesStalker moveStalker;

        private DbPartie dbPartie;

        // Creation Partie
        public RemoteGame (int size, Player blackPlayer, Player whitePlayer = null)
            : base(size, whitePlayer, blackPlayer)
        {
            this.ApplicationDataContext = new DbGobansDataContext();

            DbJoueur black = DbJoueur.ConnectOrCreatePlayer(blackPlayer.Name, ApplicationDataContext);
            DbPartie partie = new DbPartie { DbJoueurs_IdJoueurNoir = black, HeureDebut = DateTime.Now };
            // Création en base
            if (whitePlayer != null)
            {
                DbJoueur white = DbJoueur.ConnectOrCreatePlayer(whitePlayer.Name, ApplicationDataContext);
                partie.DbJoueurs_IdJoueurBlanc = white;
                this.Status = "playing";
                dbPartie.EtatPartie = "playing";
            }
            else
            {
                this.Status = "pending";
                dbPartie.EtatPartie = "pending";
            }

            this.ApplicationDataContext.DbParties.InsertOnSubmit(partie);
            this.dbPartie = partie;

            this.ApplicationDataContext.SubmitChanges();
            moveStalker = new RemoteMovesStalker(this.dbPartie);
            moveStalker.Observers.Add(this);
        }

        //Rejoindre déjà commencée
        public RemoteGame (DbPartie partie)
            : base(9, ToLocalPlayerModel(partie.DbJoueurs_IdJoueurBlanc, PlayerColor.White), ToLocalPlayerModel(partie.DbJoueurs_IdJoueurNoir, PlayerColor.Black))
        {
            this.ApplicationDataContext = new DbGobansDataContext();
            this.dbPartie = partie;

            // Récupération des coups
            foreach (DbCoup dbCoup in this.dbPartie.DbCoups.OrderBy(p => p.HeureCoup))
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
            
            moveStalker = new RemoteMovesStalker(this.dbPartie);
            moveStalker.Observers.Add(this);
        }

        private static Player ToLocalPlayerModel (DbJoueur dbJoueur, PlayerColor playerColor)
        {
            return new Player(dbJoueur.Nom, playerColor);
        }

        public void EndGame ()
        {
            base.EndGame();
            this.dbPartie.EtatPartie = "over";
            //using (DbGobansDataContext context = ApplicationDataContext)
            //{
            this.dbPartie.EtatPartie = "over";
            ApplicationDataContext.SubmitChanges();
            //}
            this.ApplicationDataContext.Dispose();
        }

        public void PutRock (int x, int y)
        {
            base.PutRock(x, y);
            // Enregistrer en base
            //using (DbGobansDataContext context = this.ApplicationDataContext)
            //{
            var player = DbJoueur.ConnectOrCreatePlayer(this.CurrentPlayer.Name, ApplicationDataContext);
            this.dbPartie.PoserPion(x, y, player);
            ApplicationDataContext.SubmitChanges();
            //}
        }

        public void PasserTour ()
        {
            base.PasserTour();
            // Enregistrer en base
            //using (DbGobansDataContext context = this.ApplicationDataContext)
            //{
            var player = DbJoueur.ConnectOrCreatePlayer(this.CurrentPlayer.Name, ApplicationDataContext);
            this.dbPartie.PasserTour(player);
            ApplicationDataContext.SubmitChanges();
            //}
        }

        public void Update ()
        {
            base.Update();
            if (this.Status == "playing")
            {
                var remotePlayer = CurrentPlayer as RemotePlayer;
                if (remotePlayer != null)
                {
                    remotePlayerPlayed.WaitOne();
                    DbCoup lastCoup = moveStalker.LastCoupPlayed;
                    // Jouer le coup sur l'interface
                    if (lastCoup.X.HasValue && lastCoup.Y.HasValue)
                    {
                        this.UIManager.PoserPion(remotePlayer, (int)lastCoup.X.Value, (int)lastCoup.Y);
                    }
                    else
                    {
                        base.PasserTour();
                    }
                }

            }

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
            remotePlayerPlayed.Set();
        }

        #endregion
    }
}
