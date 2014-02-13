using System;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.ObjetsDeJeu
{
		public class Pattern
		{
				private Coordonnees[] _path;
				private int currentIndex = 0;

				public Pattern (params Coordonnees[] list)
				{
					_path = new Coordonnees[list.Length];
					_path [0] = list [0];
					for ( int i = 1 ; i < list.Length ; i++ )
					{
						_path[i] = new Coordonnees(_path[i-1].X+list[i].X,_path[i-1].Y+list[i].Y);
					}
				}

				public Coordonnees getNextCoord()
				{
					if (currentIndex < _path.Length) {
						Coordonnees c = _path [currentIndex];
						currentIndex++;
						return c;
					} else {
						return null;
					}
					
				}

				public bool isPatternOver()
				{
					return currentIndex >= _path.Length;
				}
		}
}

