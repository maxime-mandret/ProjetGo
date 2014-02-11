using Assets.ObjetsDeJeu;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Collections.Generic;
using System;

namespace Assets.GameUtils
{
    public class UnityUiMananger : IUiManager
    {
		private bool isToolTipDisplayed = false;

		public bool IsToolTipDisplayed(){
			return this.isToolTipDisplayed;
		}

        public void PoserPion(Player p, int x, int y)
        {
            var pion = Object.Instantiate(Camera.main.GetComponent<PlayerLogic>().lepion, GameObject.Find("inter_" + x + "_" + y).transform.position + Vector3.up, Quaternion.identity) as GameObject;
            if (pion != null)
            {
                pion.name = "pion";
                pion.renderer.material.color = p.Color == PlayerColor.Black ? Color.black : Color.white;
				pion.GetComponent<StopOnCollision>().baseCouleur = p.Color == PlayerColor.Black ? Color.black : Color.white;;
            }
        }

		public void DisplayToolTip(string s)
		{
				string[] st = s.Split ('_');
				int x = Convert.ToInt32 (st.GetValue (1));
				int y = Convert.ToInt32 (st.GetValue (2));
				Vector3 casePos = GameObject.Find (s).transform.position;
				Goban gobin = GameObject.Find ("Game Logic").GetComponent<GameLogicDisplay> ().Game.Goban;
				Intersection i = gobin [x, y];
				List<Groupe> g = (List<Groupe>)gobin.Groupes;
				Groupe leGroupe = g.Find (gr => gr.Contains (i));
				if(leGroupe != null)
				{
					foreach (Intersection inter in leGroupe) {
						if(GameObject.Find(String.Format("inter_{0}_{1}",inter.Coord.X,inter.Coord.Y)).transform.childCount > 0)
						{
							GameObject.Find (String.Format ("inter_{0}_{1}", inter.Coord.X, inter.Coord.Y)).transform.GetChild (0).GetComponent<StopOnCollision> ().Select ();
						}
					}
					isToolTipDisplayed = true;
				}

		}

		public void HideToolTip()
		{
			Goban gobin = GameObject.Find("Game Logic").GetComponent<GameLogicDisplay>().Game.Goban;
			List<Groupe> g = (List<Groupe>)gobin.Groupes;
			foreach (Groupe gr in g) {
				foreach (Intersection inter in gr) {
					if(GameObject.Find(String.Format("inter_{0}_{1}",inter.Coord.X,inter.Coord.Y)).transform.childCount > 0)
					{
						GameObject.Find(String.Format("inter_{0}_{1}",inter.Coord.X,inter.Coord.Y)).transform.GetChild(0).GetComponent<StopOnCollision>().Deselect();
					}
				}
			}
			isToolTipDisplayed = false;
		}
    }
}
