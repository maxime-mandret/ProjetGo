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

        public RemoteGame (int size, Player blackPlayer, Player whitePlayer = null)
            : base(size, whitePlayer, blackPlayer)
        {
            this.ApplicationDataContext = new DbGobansDataContext();

            DbJoueur black = DbJoueur.ConnectOrCreatePlayer(blackPlayer.Name, ApplicationDataContext);
            DbPartie partie = new DbPartie { DbJoueurs_IdJoueurNoir = black };
            // Création en base
            if (whitePlayer != null)
            {
                DbJoueur white = DbJoueur.ConnectOrCreatePlayer(whitePlayer.Name, ApplicationDataContext);
                this.ApplicationDataContext.DbJoueurs.InsertOnSubmit(white);
                partie.DbJoueurs_IdJoueurBlanc = white;
            }

            DbGoban goban = new DbGoban { DbPartie = partie, JoueurEnCour = partie.IdJoueurNoir };
            
            this.ApplicationDataContext.DbJoueurs.InsertOnSubmit(black);
            this.ApplicationDataContext.DbParties.InsertOnSubmit(partie);
            this.dbPartie = partie;
            this.ApplicationDataContext.DbGobans.InsertOnSubmit(goban);
            this.ApplicationDataContext.SubmitChanges();
            moveStalker = new RemoteMovesStalker(goban);
            moveStalker.Observers.Add(this);
        }

        //public RemoteGame (DbGoban goban)
        //{
        //    this.ApplicationDataContext = new DbGobansDataContext();

        //    // Récupération des coups
        //    foreach (DbPion dbPion in goban.DbPions.OrderBy(p => p.NumeroCoup))
        //    {
        //        // Méthode classe mère car on ne sauvegarde pas le mouvement (vu qu'il existe deja)
        //        base.PutRock(dbPion.PositionX, dbPion.PositionY);
        //    }            

        //    moveStalker = new RemoteMovesStalker(goban);
        //}

        public void EndGame ()
        {
            base.EndGame();
            // 
            this.ApplicationDataContext.Dispose();
        }

        public void PutRock (int x, int y)
        {
            base.PutRock(x, y);
            // Enregistrer en base

        }

        protected void ChangeCurrentPlayer ()
        {
            base.ChangeCurrentPlayer();
            // Enregistrer en base
        }

        public void PasserTour ()
        {
            base.PasserTour();
            // Enregistrer en base
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
                    DbPion lastCoup = moveStalker.LastCoupPlayed;
                    // Jouer le coup sur l'interface
                    this.UIManager.PoserPion(remotePlayer, lastCoup.PositionX, lastCoup.PositionY);
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

        public void ObservedNotified(RemoteMovesStalker remoteMovesStalker)
        {
            remotePlayerPlayed.Set();
        }

        #endregion
    }
}
