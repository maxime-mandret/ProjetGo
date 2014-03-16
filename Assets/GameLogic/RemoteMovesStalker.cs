using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Assets.ObjetsDeJeu;
using DbGobansContext;
using Devart.Data.Linq;
using System.Timers;

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
            if (this._observed.Status == "playing" && this._observed.CurrentPlayer as RemotePlayer != null)
            {
                this._observed.ApplicationDataContext.Refresh(RefreshMode.OverwriteCurrentValues, this._observed.DbPartie);
                var baseLastCoup = this._observed.DbPartie.GetLastCoup();
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
