using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using Assets.ObjetsDeJeu;

using Assets.Db;

namespace Assets.GameLogic
{
    public class RemotePlayerStalker
    {
        private Timer _stalkerTimer;
        private RemoteGame _observed;
        public DbJoueur WaitedPlyer { get; set; }

        public List<IObserver<RemotePlayerStalker>> Observers { get; set; }

        public RemotePlayerStalker (RemoteGame partie)
        {
            this.Observers = new List<IObserver<RemotePlayerStalker>>();
            _observed = partie;

            this._stalkerTimer = new Timer(1000);
            _stalkerTimer.Elapsed += new ElapsedEventHandler(StalkerCheckOut);
            _stalkerTimer.AutoReset = true;
            _stalkerTimer.Enabled = true;
            _stalkerTimer.Start();
        }

        void StalkerCheckOut (object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine(string.Format("Stalker callback entered !"));
            if (this._observed.Status == GameStatuts.pendingPlayer)
            {
                this._observed.ApplicationDataContext.Refresh(Devart.Data.Linq.RefreshMode.OverwriteCurrentValues, this._observed.DbPartie);
                var waitingPlayer = this._observed.DbPartie.DbJoueurs_IdJoueurBlanc;
                if (waitingPlayer != null)
                {
                    this.WaitedPlyer = waitingPlayer;
                    Observers.ForEach(obs => obs.ObservedNotified(this));
                }
            }
        }
    }
}
