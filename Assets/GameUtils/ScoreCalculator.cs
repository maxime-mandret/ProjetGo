using System;
using Assets.ObjetsDeJeu;
using UnityEngine;
namespace Assets.GameUtils
{
		public class ScoreCalculator
		{
				public double WhiteFinalScore { get; set;}
				public double BlackFinalScore { get; set;}
				private Player _whitePlayer;
				private Player _blackPlayer;

				public ScoreCalculator (Player whitep,Player blackp)
				{
					this.WhiteFinalScore = 0;
					this.BlackFinalScore = 0;
					this._whitePlayer = whitep;
					this._blackPlayer = blackp;
				}

				public bool CalculateFinalScore(Goban goban,bool display = false)
				{
					int score = 0;
					for(int x = 0; x < goban.Size; x++)
					{
						for(int y = 0; y < goban.Size; y++)
						{
							if(goban[x, y].Owner == this._whitePlayer)
							{
								this.WhiteFinalScore++;
							}else if(goban[x, y].Owner == this._blackPlayer)
							{
								this.BlackFinalScore++;
							}else if(goban[x, y].Owner == null)
							{
								//Debug.Log (string.Format("goban[x, y] : {0},{1} - Intersection  : x,y {2},{3}",x,y,goban[x, y].Coord.X,goban[x, y].Coord.Y));
								//FONCTIONNE PAS ?!int lecount = goban.GetAround(goban[x, y]).FindAll(pt=>pt.Owner != null).Count;
								int lecount = goban.GetAround(goban[x, y]).Count;
								if(lecount > 0)
								{
									if(lecount > 1)
									{
										int bcount,wcount = bcount = 0;
										foreach(Intersection i in goban.GetAround(goban[x, y]))
										{
											string leown = "";
											if(i.Owner == this._whitePlayer)
											{
												wcount++;
											}else if(i.Owner == this._blackPlayer)
											{
												bcount++;
											}
											
										}
										if(wcount > bcount)
										{
											if(bcount == 0 && wcount == 1)
											{
												this.WhiteFinalScore+=0.5;
												if(display){
													GameObject.Find(string.Format("inter_{0}_{1}",x,y)).GetComponent<Case>().ScoreColor = new Color(255f,255f,255f,0.4f);
												}
												
											}
											else{
												this.WhiteFinalScore++;
												if(display){
													GameObject.Find(string.Format("inter_{0}_{1}",x,y)).GetComponent<Case>().ScoreColor = new Color(255f,255f,255f,1f);
												}
												
											}
											
										}else if(wcount < bcount)
										{
											
											if(wcount == 0 && bcount == 1)
											{
												//Un seul NOIR
												this.BlackFinalScore+=0.5;
												if(display){
													GameObject.Find(string.Format("inter_{0}_{1}",x,y)).GetComponent<Case>().ScoreColor = new Color(0f,0f,0f,0.3f);
												}
											}
											else{
												this.BlackFinalScore++;
												if(display){
													GameObject.Find(string.Format("inter_{0}_{1}",x,y)).GetComponent<Case>().ScoreColor = new Color(0f,0f,0f,1f);
												}
											}
											
										}else
										{
										
										}
									}
								}
							}
						}
					}
					return true;
				}
		}
}

