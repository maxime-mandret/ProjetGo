using System.Linq;
using UnityEngine;

namespace Assets.ObjetsDeJeu
{
	public class RandomIaPlayer : Player, IAPlayer
	{
		public RandomIaPlayer(string name, PlayerColor color) : base(name,color)
		{
		}
		
		public Coordonnees GetBestMove(Goban goban)
		{
			
			if(goban.MoveList.Count > 0)
			{
				if(goban.MoveList.Count() < 20 && Random.Range(0, 3) == 1)
				{
					int rx = Random.Range(0, 8);
					int ry = Random.Range(0, 8);
					return new Coordonnees(rx, ry);
				} else
				{
					Move lastMove = goban.MoveList.ElementAt(Random.Range(0, goban.MoveList.Count() - 1));
					int xOffest = Random.Range(-1, 2);
					int yOffest = Random.Range(-1, 2);				
					return new Coordonnees(lastMove.Coord.X + (int)xOffest, lastMove.Coord.Y + (int)yOffest);
				}
			} else
			{
				int rx = Random.Range(0, 9);
				int ry = Random.Range(0, 9);
				return new Coordonnees(rx, ry);
			}
		}
	}
}

