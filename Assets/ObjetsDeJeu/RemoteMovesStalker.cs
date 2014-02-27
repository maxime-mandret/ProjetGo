//using System.Collections.Generic;
//using System.ComponentModel;
//using DbGobansContext;
//
//namespace Assets.ObjetsDeJeu
//{
//    public class RemoteMovesStalker : IObservable<RemoteMovesStalker>
//    {
//        private DbGoban _observed;
//        public DbPion LastCoupPlayed { get; set; }
//
//        public List<IObserver<RemoteMovesStalker>> Observers { get; set; }
//
//        public RemoteMovesStalker (DbGoban goban)
//        {
//            _observed = goban;
//            _observed.DbPions.ListChanged += DbPionsOnListChanged;
//        }
//
//        private void DbPionsOnListChanged (object sender, ListChangedEventArgs listChangedEventArgs)
//        {
//            var baseLastCoup = _observed.GetLastCoup();
//            if (baseLastCoup.NumeroCoup > LastCoupPlayed.NumeroCoup)
//            {
//                LastCoupPlayed = baseLastCoup;
//                Observers.ForEach(obs => obs.ObservedNotified(this));
//            }
//        }
//    }
//}
