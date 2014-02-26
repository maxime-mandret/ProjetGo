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
			Player p = this[0].Owner;
			if(isAtari(goban))
			{
				alive = false;
			}else
			{
				int freelibs = 0;
				foreach(Intersection i in this)
				{
					List<Intersection> libertes = goban.GetLibertes(i);
					foreach(Intersection lib in libertes)
					{
						List<Intersection> tmp = goban.GetAround(i);
						if(tmp.Count>0)
						{
							int pn = 0;
							int an = 0;
							int n = 0;
							foreach(Intersection ii in tmp)
							{
								if(ii.Owner == null)
								{
									n++;
								}else if(ii.Owner == p)
								{
									pn++;
								}else if(ii.Owner != p)
								{
									an++;
								}
							}
							if(pn>an)
							{
								freelibs++;
							}
						}
					}
				}
				if(freelibs<2)
				{
					alive = false;
				}
			}
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
