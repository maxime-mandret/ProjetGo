using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DbGobansContext
{
    public partial class DbPartie
    {
        public static List<DbPartie> GetAllRunningGames (DbGobansDataContext context)
        {
            var runningGames = context.DbParties.Where(g => g.EtatPartie == "En cours");
            return runningGames.ToList();
        }

        public static List<DbPartie> GetPendingGames (DbGobansDataContext context)
        {
            var incompleteGames = new List<DbPartie>();
            incompleteGames.AddRange(context.DbParties.Where(part => part.DbJoueurs_IdJoueurNoir == null || part.DbJoueurs_IdJoueurBlanc == null));
            return incompleteGames;
        }

        public void AddPlayer (DbJoueur joueur)
        {
            if (this.DbJoueurs_IdJoueurNoir == null)
            {
                this.DbJoueurs_IdJoueurNoir = joueur;
            }
            else if (this.DbJoueurs_IdJoueurBlanc == null)
            {
                this.DbJoueurs_IdJoueurBlanc = joueur;
            }
            else
            {
                throw new InvalidOperationException("Game is full");
            }

            if (this.DbJoueurs_IdJoueurBlanc != null && this.DbJoueurs_IdJoueurNoir != null)
            {
                this.EtatPartie = "En cours";
                this.HeureDebut = DateTime.Now;
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
