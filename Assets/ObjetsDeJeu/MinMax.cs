using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.ObjetsDeJeu
{
    class MinMax : Player, IAPlayer
    {
        public MinMax(string name, PlayerColor color) : base(name, color)
        {
            // @TODO
        }

        public Coordonnees GetBestMove(Goban goban)
        {

            throw new NotImplementedException();
        }

        private int getMin(int profondeur)
        {
            return 0;
        }

        private int getMax(int profondeur)
        {
            return 0;
        }

        private int getSeries()
        {
            return 0;
        }
    }
}
