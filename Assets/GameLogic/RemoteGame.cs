using System;
using System.Linq;
using System.Threading;
using Assets.GameUtils;
using Assets.GameUtils.Sgf;
using Assets.ObjetsDeJeu;
using DbGobansContext;

namespace Assets.GameLogic
{
	public class RemoteGame : Game, IDisposable, IObserver<RemoteMovesStalker>
	{
	    public static RemoteGame Instance
	    {
	        get { return _instance; }
	        set { _instance = value; }
	    }

        private static RemoteGame _instance;
        public DbGobansDataContext ApplicationDataContext { get; set; }
        private EventWaitHandle remotePlayerPlayed = new EventWaitHandle(false, EventResetMode.AutoReset);
		
		public RemoteGame(int size, Player whitePlayer, Player blackPlayer) : base(size, whitePlayer, blackPlayer)
		{
		    Instance = this;
            this.ApplicationDataContext = new DbGobansDataContext();
		}
		
		public void EndGame()
		{
		    base.EndGame();
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

		public void Update()
		{
            base.Update();
			if(this.Status == "playing")
			{
                var remotePlayer = CurrentPlayer as RemotePlayer;
			    if (remotePlayer != null)
			    {
                    remotePlayerPlayed.WaitOne();
                    // Recherché le dernier coup
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

        public void ObservedNotified<T> (T observedState)
        {
            remotePlayerPlayed.Set();
        }

        #endregion
    }

}
