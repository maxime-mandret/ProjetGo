using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.ObjetsDeJeu
{
	public class Groupe : List<Intersection>
	{
		public bool isAlive(Goban goban)
		{
			bool alive = true;
			//Player p = this[0].Owner;
			int totalibs = 0;
			if(isAtari(goban))
			{
				alive = false;
			}else
			{
				List<Intersection> freelibs = new List<Intersection>();
				foreach(Intersection i in this)
				{
					freelibs.AddRange(goban.GetLibertes(i));
//					foreach(Intersection lib in libertes)
//					{
//						List<Intersection> tmp = goban.GetAround(lib);
//						if(tmp.Count>0)
//						{
//							int pn = 0;
//							int an = 0;
//							int n = 0;
//							foreach(Intersection ii in tmp)
//							{
//								if(ii.Owner == null)
//								{
//									n++;
//								}else if(ii.Owner == p)
//								{
//									pn++;
//								}else if(ii.Owner != p)
//								{
//									an++;
//								}
//							}
//							if(pn>an || n > an)
//							{
//								freelibs++;
//							}
//						}
//					}
					Debug.Log("x: "+i.Coord.X+" y: "+i.Coord.Y+" libs: "+goban.GetLibertes(i).Count);
				}
				if(freelibs.Count<2)
				{
					alive = false;
				}
				totalibs = freelibs.Count;
			}
			
			Debug.Log("-- groupe "+((alive==true)?"vivant":"mort")+" avec "+totalibs+" libertes");
			return alive;
		}
		
		public bool isAtari(Goban goban)
		{
			bool isAtari = true;
			foreach(Intersection i in this)
			{
				if(!goban.IsAtari(i))
				{
					isAtari = false;
				}
				
			}
			return isAtari;
		}

	}

}
