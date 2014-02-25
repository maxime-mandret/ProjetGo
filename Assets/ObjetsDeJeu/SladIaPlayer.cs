using System;
using System.Collections.Generic;
using System.Linq;


namespace Assets.ObjetsDeJeu
{
	public class SladIaPlayer : Player, IAPlayer
	{
	    private int _currentIndex;
		private Pattern _currentPattern;
		private readonly Pattern[] _patterns;
		private Coordonnees _currentPoint;
	    private Coordonnees _nextCoord;

		public SladIaPlayer(string name, PlayerColor color) : base(name,color)
		{
			_patterns = new Pattern[4];
			_patterns [0] = new Pattern(new Coordonnees(3,0),new Coordonnees(0,1),new Coordonnees(0,1),new Coordonnees(0,1),new Coordonnees(-1,0),new Coordonnees(-1,0),new Coordonnees(-1,0));
			_patterns [2] = new Pattern (new Coordonnees (5, 0), new Coordonnees (0, 1), new Coordonnees (0, 1), new Coordonnees (0, 1), new Coordonnees (1, 0), new Coordonnees (1, 0), new Coordonnees (1, 0));
			_patterns [3] = new Pattern (new Coordonnees (5, 5),new Coordonnees(1,0),new Coordonnees(0,1),new Coordonnees(-1,1),new Coordonnees(-1,-1),new Coordonnees(0,-1));
			_patterns [1] = new Pattern (new Coordonnees (0, 5),new Coordonnees (1, 0),new Coordonnees (0, 1),new Coordonnees (0, 1),new Coordonnees (0, 1),new Coordonnees (0, 1),new Coordonnees (-1, 0),new Coordonnees (-1, 0),new Coordonnees (-1, 0),new Coordonnees (0, -1),new Coordonnees (1, 0),new Coordonnees (1,0));
		}
		
		public Coordonnees GetBestMove(Goban goban)
		{
            // Si on a plus aucun coup à jouer on retourne null
            bool isOver = true;
		    foreach (Pattern t in _patterns)
		    {
		        if (!t.isPatternOver())
		        {
		            isOver = false;
		        }
		    }
		    if (isOver)
		    {
		        return null;
		    }

		    // On boucle jusqu'à ce qu'on trouve une place pour jouer
            bool canPlay = false;
		    while (!canPlay)
		    {
                // Choix d'une pattern
                var random = new Random();
                int randomNumber = random.Next(0, _patterns.Length);
                _currentPattern = _patterns[randomNumber];
                _currentIndex = randomNumber;

                // Si la pattern n'est pas finie on tente de la terminer
		        if (!_currentPattern.isPatternOver())
		        {
		                // On récupère la prochaine coordonnée
                    _nextCoord = _currentPattern.getNextCoord();
		            if (_nextCoord != null && goban.CanPlay(_nextCoord.X, _nextCoord.Y))
		            {
		                canPlay = true;
		            }
		        }
		        else
		        {
		            // On vérifie si les autres ne sont pas vides pour sortir
                    isOver = true;
                    foreach (Pattern t in _patterns)
                    {
                        if (!t.isPatternOver())
                        {
                            isOver = false;
                        }
                    }
                    if (isOver)
                    {
                        return null;
                    }
		        }
		    }
		    return _nextCoord;
		}
	}
}

