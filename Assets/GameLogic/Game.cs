using System;
using System.Linq;
using Assets.GameUtils;
using Assets.GameUtils.Sgf;
using Assets.ObjetsDeJeu;
using DbGobansContext;

namespace Assets.GameLogic
{
	public class Game
	{
	    public static Game Instance
	    {
	        get { return _instance; }
	        set { _instance = value; }
	    }

        private static Game _instance;

		protected Player _whitePlayer;
		protected Player _blackPlayer;
		protected Player _currPlayer;
		protected Goban _goban;
        public int NbTour { get; set; }
        public string Status { get; set; }
        protected IUiManager UIManager;
		protected ScoreCalculator _scalc;
	    
	    public double WhiteScore { get; set; }
		public double BlackScore { get; set; }
		
		public Game(int size, Player blackPlayer, Player whitePlayer)
		{
			this.Goban = new Goban(size);
            this.UIManager = new UnityUiMananger();
			this.WhitePlayer = whitePlayer;
			this.BlackPlayer = blackPlayer;
			this.CurrentPlayer = BlackPlayer;
			this.Status = "playing";
			_scalc = new ScoreCalculator (_whitePlayer,_blackPlayer);
		}

        public Game (string path)
        {
            var file = new SgfFile(path);
            Goban = new Goban(file.Header.Size);
            WhitePlayer = file.Header.WhitePlayer;
            BlackPlayer = file.Header.BlackPlayer;
            this.CurrentPlayer = file.Moves.First().Player;
            foreach (var move in file.Moves)
            {
                this.Goban.PutRock(move);
            }
        }
		
		public virtual void PasserTour()
		{
			this.CurrentPlayer.NbAbandonSuccessifs++;
			if(_whitePlayer.NbAbandonSuccessifs >= 1 && _blackPlayer.NbAbandonSuccessifs >= 1)
			{
				EndGame();
			} else
			{
				this.ChangeCurrentPlayer();
				this.Update();
			}
		}
        
		public virtual void EndGame()
		{
			_scalc.CalculateFinalScore (this.Goban,true);
			this.BlackScore = _scalc.BlackFinalScore;
			this.WhiteScore = _scalc.WhiteFinalScore;
			this.Status = "over";
		}

		public virtual void Update()
		{
			if(this.Status == "playing")
			{

				var randomPlayer = CurrentPlayer as RandomIaPlayer;
				var nbEssais = 0;
				if(randomPlayer != null)
				{
					Coordonnees c = null;
					do
					{
						c = randomPlayer.GetBestMove(this.Goban);
						nbEssais++;
					} while (!Goban.CanPlay(c.X, c.Y) && nbEssais <= 20);

					if(!Goban.CanPlay(c.X, c.Y))
					{
						PasserTour();
					} else
					{
						this.PutRock(c.X, c.Y);
					}
					NbTour++;
					return;
				}

				var sladIaPlayer = CurrentPlayer as SladIaPlayer;
				if(sladIaPlayer != null)
				{
					Coordonnees c = null;
					c = sladIaPlayer.GetBestMove(this.Goban);
					
					if(c == null || !Goban.CanPlay(c.X, c.Y))
					{
						PasserTour();
					} else
					{
						this.PutRock(c.X, c.Y);
					}
					NbTour++;
				}

			}

		}
		
		public virtual void PutRock(int x, int y)
		{
			this.CurrentPlayer.NbAbandonSuccessifs = 0;
			if(this.Goban.CanPlay(x, y))
			{
				this.Goban.PutRock(CurrentPlayer, x, y);
                this.UIManager.PoserPion(CurrentPlayer, x, y);
				this.ChangeCurrentPlayer();
			}
		}
		
		protected virtual void ChangeCurrentPlayer()
		{
		    this.CurrentPlayer = this.CurrentPlayer.Equals(WhitePlayer) ? BlackPlayer : WhitePlayer;
		}

	    public Goban Goban {
			get { return _goban; }
			set { _goban = value; }
		}
		
		public Player BlackPlayer {
			get { return _blackPlayer; }
			private set { _blackPlayer = value; }
		}
		
		public Player WhitePlayer {
			get { return _whitePlayer; }
			private set { _whitePlayer = value; }
		}
		
		public Player CurrentPlayer {
			get { return _currPlayer; }
			private set { _currPlayer = value; }
		}
    }

}
