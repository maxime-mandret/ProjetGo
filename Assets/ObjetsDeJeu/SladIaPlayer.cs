using System.Linq;
using UnityEngine;


namespace Assets.ObjetsDeJeu
{
	public class SladIaPlayer : Player, IAPlayer
	{
		private Pattern _currentPattern = null;
		private int indexPattern = 0;
		private Pattern[] _patterns;
		private Coordonnees _currentPoint;
		public SladIaPlayer(string name, PlayerColor color) : base(name,color)
		{
			_patterns = new Pattern[4];
			_patterns [0] = new Pattern(new Coordonnees(3,0),new Coordonnees(0,1),new Coordonnees(0,1),new Coordonnees(0,1),new Coordonnees(-1,0),new Coordonnees(-1,0),new Coordonnees(-1,0));
			_patterns [2] = new Pattern (new Coordonnees (5, 0), new Coordonnees (0, 1), new Coordonnees (0, 1), new Coordonnees (0, 1), new Coordonnees (1, 0), new Coordonnees (1, 0), new Coordonnees (1, 0));
			_patterns [3] = new Pattern (new Coordonnees (5, 5),new Coordonnees(1,1),new Coordonnees(0,1),new Coordonnees(-1,1),new Coordonnees(-1,-1),new Coordonnees(0,-1));
			_patterns [1] = new Pattern (new Coordonnees (0, 5),new Coordonnees (1, 0),new Coordonnees (0, 1),new Coordonnees (0, 1),new Coordonnees (-1, 0),new Coordonnees (1, 1),new Coordonnees (1, -1),new Coordonnees (1, 0),new Coordonnees (0, 1));
		}
		
		public Coordonnees GetBestMove(Goban goban)
		{
			if (_currentPattern == null) {
				_currentPattern = _patterns[indexPattern];
			}
			Coordonnees c = _currentPattern.getNextCoord ();

			if (c != null && !goban.CanPlay (c.X, c.Y) && !_currentPattern.isPatternOver ()) {
				c = _currentPattern.getNextCoord ();
			}

			if (indexPattern + 1 >= _patterns.Length && c == null) {
				return null;
			}
			if (c == null && indexPattern + 1 < _patterns.Length || !goban.CanPlay(c.X,c.Y)) {
					indexPattern++;
					_currentPattern = _patterns [indexPattern];
					c = _currentPattern.getNextCoord ();
			}

			return c;
		}
	}
}

