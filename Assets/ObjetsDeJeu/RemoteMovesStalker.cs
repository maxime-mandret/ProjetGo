using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Assets.GameLogic;
using DbGobansContext;

namespace Assets.ObjetsDeJeu
{
    public class RemoteMovesStalker : IObservable<RemoteMovesStalker>
    {
        private DbGoban _observed;
        public DbPion LastCoupPlayed { get; set; }

        public List<IObserver<RemoteMovesStalker>> Observers { get; set; }

        public RemoteMovesStalker (DbGoban goban)
        {
            goban.DbPions.ListChanged += DbPionsOnListChanged;
        }

        private void DbPionsOnListChanged (object sender, ListChangedEventArgs listChangedEventArgs)
        {
            var baseLastCoup = _observed.GetLastCoup();
            if (baseLastCoup.NumeroCoup > LastCoupPlayed.NumeroCoup)
            {
                LastCoupPlayed = baseLastCoup;
                Observers.ForEach(obs => obs.ObservedNotified(this));
            }
            
        }
    }
}
