using System.Collections.Generic;
using System.Diagnostics;
using Assets.ObjetsDeJeu;
using System.Timers;
using Assets.Db;

namespace Assets.GameLogic
{
    public class RemoteMovesStalker
    {
        private RemoteGame _observed;
        
        public DbCoup LastCoupPlayed { get; set; }

        private Timer _stalkerTimer;

        public List<IObserver<RemoteMovesStalker>> Observers { get; set; }

        public RemoteMovesStalker (RemoteGame partie)
        {
            this._stalkerTimer = new Timer(1000);
            _stalkerTimer.Elapsed += new ElapsedEventHandler(StalkerCheckOut);
            _stalkerTimer.AutoReset = true;
            _stalkerTimer.Enabled = true;
            _stalkerTimer.Start();
            this.Observers = new List<IObserver<RemoteMovesStalker>>();
            _observed = partie;
        }

        void StalkerCheckOut (object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine(string.Format("Stalker callback entered !"));
            if (this._observed.Status == GameStatuts.playing && this._observed.CurrentPlayer as RemotePlayer != null)
            {
                //this._observed.ApplicationDataContext.DbParties.Attach(this._observed.DbPartie);
                this._observed.ApplicationDataContext.Refresh(Devart.Data.Linq.RefreshMode.OverwriteCurrentValues, this._observed.ApplicationDataContext.DbParties);
                var baseLastCoup = this._observed.DbPartie.GetLastCoup();
                // Si c'est le premier coup
                if (LastCoupPlayed == null)
                {
                    LastCoupPlayed = baseLastCoup;
                    Observers.ForEach(obs => obs.ObservedNotified(this));
                }
                else if(baseLastCoup.HeureCoup > LastCoupPlayed.HeureCoup)
                {
                    LastCoupPlayed = baseLastCoup;
                    Observers.ForEach(obs => obs.ObservedNotified(this));
                }
            }
        }
    }
}
