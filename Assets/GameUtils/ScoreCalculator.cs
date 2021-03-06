using System;
using Assets.ObjetsDeJeu;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.GameUtils
{
    public class ScoreCalculator
    {
        public double WhiteFinalScore { get; set; }
        public double BlackFinalScore { get; set; }
        private Player _whitePlayer;
        private Player _blackPlayer;
        private Player _noPlayer;
        private Intersection[,] _tabIntersect;
		private UnityUiMananger ui;
        public ScoreCalculator(Player whitep, Player blackp)
        {
            this.WhiteFinalScore = 0;
            this.BlackFinalScore = 0;
            this._whitePlayer = whitep;
            this._blackPlayer = blackp;
            this._noPlayer = new Player("noplayer", PlayerColor.Black);
			ui = new UnityUiMananger ();
        }
		public bool CalculateFinalScore(Goban goban, bool display = false)
		{
			//On élimine d'abord les groupes morts
			foreach (Groupe g in goban.Groupes) {
				if(!g.isAlive(goban))
				{
					//Groupe à suppriemer et on reset les owner ?
					if(g[0].Owner == _whitePlayer)
					{
						BlackFinalScore += g.Count;
					}else
					{
						WhiteFinalScore += g.Count;
					}
					foreach(Intersection i in g)
					{
						ui.deletePion(i.Coord.X,i.Coord.Y);
					}
				}else
				{
					//TODO rien faire ici et refaire du case par case avec 3 mini
					this.ajoutPoint(g);
				}
			}
			
			return true;
		}

		public void ajoutPoint(Groupe g)
		{
			if (g [0].Owner == _whitePlayer) {
				WhiteFinalScore += g.Count;
			} else {
				BlackFinalScore += g.Count;
			}
		}
    }
}

