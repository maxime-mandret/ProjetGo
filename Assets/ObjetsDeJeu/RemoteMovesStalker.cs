using System.Collections.Generic;
using System.ComponentModel;
using DbGobansContext;

namespace Assets.ObjetsDeJeu
{
    public class RemoteMovesStalker
    {
        private DbPartie _observed;
        public DbCoup LastCoupPlayed { get; set; }

        public List<IObserver<RemoteMovesStalker>> Observers { get; set; }

        public RemoteMovesStalker (DbPartie partie)
        {
            _observed = partie;
            _observed.DbCoups.ListChanged += DbPionsOnListChanged;
        }

        private void DbPionsOnListChanged (object sender, ListChangedEventArgs listChangedEventArgs)
        {
            var baseLastCoup = _observed.GetLastCoup();
            if (baseLastCoup.HeureCoup > LastCoupPlayed.HeureCoup)
            {
                LastCoupPlayed = baseLastCoup;
                Observers.ForEach(obs => obs.ObservedNotified(this));
            }
        }
    }
}
