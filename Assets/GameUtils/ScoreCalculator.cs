using System;
using Assets.ObjetsDeJeu;
using UnityEngine;
namespace Assets.GameUtils
{
		public class ScoreCalculator
		{
				public double WhiteFinalScore { get; set;}
				public double BlackFinalScore { get; set;}
				public ScoreCalculator ()
				{
					this.WhiteFinalScore = 0;
					this.BlackFinalScore = 0;
				}

				public bool CalculateFinalScore(Goban goban, Player whitep, Player blackp)
				{
					int score = 0;
					for(int x = 0; x < goban.Size; x++)
					{
						for(int y = 0; y < goban.Size; y++)
						{
							if(goban[x, y].Owner == whitep)
							{
								this.WhiteFinalScore++;
							}else if(goban[x, y].Owner == blackp)
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
											if(i.Owner == whitep)
											{
												wcount++;
											}else if(i.Owner == blackp)
											{
												bcount++;
											}
											
											
										}
										if(wcount > bcount)
										{
											if(bcount == 0 && wcount == 1)
											{
												this.WhiteFinalScore+=0.5;
												Case o = GameObject.Find(string.Format("inter_{0}_{1}",x,y)).GetComponent<Case>();
												o.ScoreColor =Color.green;
												o.GetComponent<Case>().ScoreColor = new Color(0,0,0,0.2f);
											}
											else{
												this.WhiteFinalScore++;
												GameObject.Find(string.Format("inter_{0}_{1}",x,y)).GetComponent<Case>().ScoreColor = new Color(0,0,0,0.8f);
											}
											
										}else if(wcount < bcount)
										{
											if(wcount == 0 && bcount == 1)
											{
												this.BlackFinalScore+=0.5;
												GameObject o = GameObject.Find(string.Format("inter_{0}_{1}",x,y));
												o.GetComponent<Case>().ScoreColor =Color.green;
												o.GetComponent<Case>().ScoreColor = new Color(255,255,255,0.2f);
											}
											else{
												this.BlackFinalScore++;
											}
											GameObject.Find(string.Format("inter_{0}_{1}",x,y)).GetComponent<Case>().ScoreColor = new Color(255,255,255,0.6f);
										}else
										{
											Debug.Log("ee");
										}

										Debug.Log(string.Format("x:{0} y:{1} wcount:{2} bcount:{3}",x,y,wcount,bcount));
									}
								}
							}
						}
					}
					return true;
				}
		}
}

