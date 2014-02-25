using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DbGobansContext
{
    public partial class DbPartie
    {
        public void AddWhitePlayer (DbJoueur joueur)
        {
            if (this.DbJoueurs_IdJoueurBlanc == null)
            {
                this.DbJoueurs_IdJoueurBlanc = joueur;
            }
        }

        public void AddBlackPlayer (DbJoueur joueur)
        {
            if (this.DbJoueurs_IdJoueurNoir == null)
            {
                this.DbJoueurs_IdJoueurNoir = joueur;
            }
        }

        public int GetNombreJoueurs()
        {
            int nb = 0;
            if (this.DbJoueurs_IdJoueurNoir != null)
            {
                nb++;
            }
            if (DbJoueurs_IdJoueurBlanc != null)
            {
                nb++;
            }
            return nb;
        }
    }
}
