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
			foreach (Groupe g in goban.Groupes) {
				Groupe prisoniers = g.getPrisoners(goban);
				foreach(Intersection i in prisoniers)
				{
					ui.deletePion(i.Coord.X,i.Coord.Y);
					i.Owner = null;
				}
				this.ajoutPoint(g[0].Owner,g.getFreeInterCount(goban));
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
		public void ajoutPoint(Player p, int nb)
		{
			if (p == _whitePlayer) {
				WhiteFinalScore += nb;
			} else {
				BlackFinalScore += nb;
			}
		}
    }
}

