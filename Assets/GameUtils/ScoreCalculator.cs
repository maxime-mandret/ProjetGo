using System;
using Assets.ObjetsDeJeu;
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

        public ScoreCalculator(Player whitep, Player blackp)
        {
            this.WhiteFinalScore = 0;
            this.BlackFinalScore = 0;
            this._whitePlayer = whitep;
            this._blackPlayer = blackp;
            this._noPlayer = new Player("noplayer", PlayerColor.Black);
        }

        public bool CalculateFinalScore(Goban goban, bool display = false)
        {

            _tabIntersect = (Intersection[,])goban.Intersections.Clone();

            int score = 0;
            for (int x = 0; x < goban.Size; x++)
            {
                for (int y = 0; y < goban.Size; y++)
                {
                    if (goban[x, y].Owner == this._whitePlayer)
                    {
                        this.WhiteFinalScore++;
                    }
                    else if (goban[x, y].Owner == this._blackPlayer)
                    {
                        this.BlackFinalScore++;
                    }
                    else if (goban[x, y].Owner == null)
                    {
                        //Debug.Log (string.Format("goban[x, y] : {0},{1} - Intersection  : x,y {2},{3}",x,y,goban[x, y].Coord.X,goban[x, y].Coord.Y));
                        //FONCTIONNE PAS ?!int lecount = goban.GetAround(goban[x, y]).FindAll(pt=>pt.Owner != null).Count;
                        int lecount = goban.GetAround(goban[x, y]).Count;
                        if (lecount > 0)
                        {
                            if (lecount > 1)
                            {
                                int bcount, wcount = bcount = 0;
                                foreach (Intersection i in goban.GetAround(goban[x, y]))
                                {
                                    string leown = "";
                                    if (i.Owner == this._whitePlayer)
                                    {
                                        wcount++;
                                    }
                                    else if (i.Owner == this._blackPlayer)
                                    {
                                        bcount++;
                                    }

                                }
                                if (wcount > bcount)
                                {
                                    if (bcount == 0 && wcount == 1)
                                    {
                                        this.WhiteFinalScore += 0.5;
                                        _tabIntersect[x, y].Owner = this._whitePlayer;
                                        if (display)
                                        {
                                            GameObject.Find(string.Format("inter_{0}_{1}", x, y)).GetComponent<Case>().ScoreColor = new Color(255f, 255f, 255f, 0.4f);
                                        }

                                    }
                                    else
                                    {
                                        this.WhiteFinalScore++;
                                        _tabIntersect[x, y].Owner = this._whitePlayer;
                                        if (display)
                                        {
                                            GameObject.Find(string.Format("inter_{0}_{1}", x, y)).GetComponent<Case>().ScoreColor = new Color(255f, 255f, 255f, 0.8f);
                                        }

                                    }

                                }
                                else if (wcount < bcount)
                                {

                                    if (wcount == 0 && bcount == 1)
                                    {
                                        //Un seul NOIR
                                        this.BlackFinalScore += 0.5;
                                        _tabIntersect[x, y].Owner = this._blackPlayer;
                                        if (display)
                                        {
                                            GameObject.Find(string.Format("inter_{0}_{1}", x, y)).GetComponent<Case>().ScoreColor = new Color(0f, 0f, 0f, 0.3f);
                                        }
                                    }
                                    else
                                    {
                                        this.BlackFinalScore++;
                                        _tabIntersect[x, y].Owner = this._blackPlayer;
                                        if (display)
                                        {
                                            GameObject.Find(string.Format("inter_{0}_{1}", x, y)).GetComponent<Case>().ScoreColor = new Color(0f, 0f, 0f, 0.8f);
                                        }
                                    }

                                }
                                else if (wcount == bcount && (wcount > 0 && bcount > 0))
                                {
                                    _tabIntersect[x, y].Owner = this._noPlayer;
                                }
                                else
                                {
                                    //Debug.Log(string.Format("{0} {1}", x, y));
                                    //print(string.Format("{0} / {1}", x, y));
                                }
                            }
                        }
                    }
                }
            }
            bool estPlein = false;
            //int nbNoOwner = goban.Size * goban.Size;
            while (!estPlein)
            {
                estPlein = true;
                for (int x = 0; x < goban.Size; x++)
                {
                    for (int y = 0; y < goban.Size; y++)
                    {
                        if (_tabIntersect[x, y].Owner == null)
                        {
                            //Debug.Log (string.Format("goban[x, y] : {0},{1} - Intersection  : x,y {2},{3}",x,y,goban[x, y].Coord.X,goban[x, y].Coord.Y));
                            //FONCTIONNE PAS ?!int lecount = goban.GetAround(goban[x, y]).FindAll(pt=>pt.Owner != null).Count;
                            int lecount = goban.GetAround(_tabIntersect[x, y]).Count;
                            if (lecount > 0)
                            {
                                if (lecount > 1)
                                {
                                    int bcount, wcount = bcount = 0;
                                    foreach (Intersection i in goban.GetAround(_tabIntersect[x, y]))
                                    {
                                        string leown = "";
                                        if (i.Owner == this._whitePlayer)
                                        {
                                            wcount++;
                                        }
                                        else if (i.Owner == this._blackPlayer)
                                        {
                                            bcount++;
                                        }

                                    }
                                    if (wcount > bcount)
                                    {
                                        if (bcount == 0 && wcount == 1)
                                        {
                                            this.WhiteFinalScore += 0.5;
                                            _tabIntersect[x, y].Owner = this._whitePlayer;
                                            if (display)
                                            {
                                                GameObject.Find(string.Format("inter_{0}_{1}", x, y)).GetComponent<Case>().ScoreColor = new Color(255f, 255f, 255f, 0.4f);
                                            }

                                        }
                                        else
                                        {
                                            this.WhiteFinalScore += 0.5;
                                            _tabIntersect[x, y].Owner = this._whitePlayer;
                                            if (display)
                                            {
                                                GameObject.Find(string.Format("inter_{0}_{1}", x, y)).GetComponent<Case>().ScoreColor = new Color(255f, 255f, 255f, 0.4f);
                                            }

                                        }

                                    }
                                    else if (wcount < bcount)
                                    {

                                        if (wcount == 0 && bcount == 1)
                                        {
                                            //Un seul NOIR
                                            this.BlackFinalScore += 0.5;
                                            _tabIntersect[x, y].Owner = this._blackPlayer;
                                            if (display)
                                            {
                                                GameObject.Find(string.Format("inter_{0}_{1}", x, y)).GetComponent<Case>().ScoreColor = new Color(0f, 0f, 0f, 0.3f);
                                            }
                                        }
                                        else
                                        {
                                            this.BlackFinalScore += 0.5;
                                            _tabIntersect[x, y].Owner = this._blackPlayer;
                                            if (display)
                                            {
                                                GameObject.Find(string.Format("inter_{0}_{1}", x, y)).GetComponent<Case>().ScoreColor = new Color(0f, 0f, 0f, 0.3f);
                                            }
                                        }

                                    }
                                    else if (wcount == bcount && (wcount > 0 && bcount > 0))
                                    {
                                        _tabIntersect[x, y].Owner = this._noPlayer;
                                    }
                                    else
                                    {
                                        //Debug.Log(string.Format("{0} {1}", x, y));									//print(string.Format("{0} / {1}", x, y));
                                    }
                                }
                            }
                        }
                        if (_tabIntersect[x, y].Owner == null)
                        {
                            estPlein = false;
                        }
                    }
                }
            }
            return true;
        }
    }
}

