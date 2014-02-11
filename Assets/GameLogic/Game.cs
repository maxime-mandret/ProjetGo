using System.Linq;
using Assets.GameUtils;
using Assets.GameUtils.Sgf;
using Assets.ObjetsDeJeu;

namespace Assets.GameLogic
{
	public class Game
	{
		private Player _whitePlayer;
		private Player _blackPlayer;
		private Player _currPlayer;
		private Goban _goban;
        public int NbTour { get; set; }
        public string Status { get; set; }
        private IUiManager UIManager;
		
		public Game(int size, Player whitePlayer, Player blackPlayer)
		{
			this.Goban = new Goban(size);
            this.UIManager = new UnityUiMananger();
			this.WhitePlayer = whitePlayer;
			this.BlackPlayer = blackPlayer;
			this.CurrentPlayer = WhitePlayer;
			this.Status = "playing";
		}
		
		public void PasserTour()
		{
			this.CurrentPlayer.NbAbandonSuccessifs++;
			if(CurrentPlayer.NbAbandonSuccessifs >= 2)
			{
				EndGame();
			} else
			{
				this.ChangeCurrentPlayer();
				this.Update();
			}
		}
		public void EndGame()
		{
			this.Status = "over";
		}
		public void Update()
		{

			if(this.Status != "over")
			{
				//Si il y a des randoms IA on les fait jouer !!!
				RandomIaPlayer player = CurrentPlayer as RandomIaPlayer;
				int nbEssais = 0;
				if(player != null)
				{
					Coordonnees c = null;
					do
					{
						c = player.GetBestMove(this.Goban);
						nbEssais++;
					} while (!Goban.CanPlay(c.X, c.Y) && nbEssais <= 10);

					if(!Goban.CanPlay(c.X, c.Y))
					{
						PasserTour();
					} else
					{
						this.PutRock(c.X, c.Y);
					}
				}
			}
			NbTour++;
		}
		
		public Game(string path)
		{
			SgfFile file = new SgfFile(path);
			Goban = new Goban(file.Header.Size);
			WhitePlayer = file.Header.WhitePlayer;
			BlackPlayer = file.Header.BlackPlayer;
			this.CurrentPlayer = file.Moves.First().Player;
			foreach(Move move in file.Moves)
			{
				this.Goban.PutRock(move);
			}
		}
		
		public void PutRock(int x, int y)
		{
			this.CurrentPlayer.NbAbandonSuccessifs = 0;
			if(this.Goban.CanPlay(x, y))
			{
				this.Goban.PutRock(CurrentPlayer, x, y);
                this.ChangeCurrentPlayer();
			    this.UIManager.PoserPion(CurrentPlayer, x, y);
			}
			
		}
		
		private void ChangeCurrentPlayer()
		{
			if(this.CurrentPlayer.Equals(WhitePlayer))
			{
				this.CurrentPlayer = BlackPlayer;
			} else
			{
				this.CurrentPlayer = WhitePlayer;
			}
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
