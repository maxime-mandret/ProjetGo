using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DbGobansContext
{
    public partial class DbPartie
    {
        public DbCoup GetLastCoup ()
        {
            DbCoup maxCoup = this.DbCoups.First(c => c.HeureCoup == this.DbCoups.Max(coup => coup.HeureCoup));
            Debug.WriteLine(string.Format("Last coup (n° {0}) on {{{1},{2}}}", maxCoup.HeureCoup, maxCoup.X, maxCoup.Y));
            return maxCoup;
        }

        public DbCoup PoserPion (int x, int y, DbJoueur joueur)
        {
            DbCoup coup = new DbCoup
            {
                DbPartie = this,
                HeureCoup = DateTime.Now,
                IdJoueur = joueur.IdJoueur,
                X = x,
                Y = y
            };
            this.DbCoups.Add(coup);
            Debug.WriteLine(string.Format("Inserting pion at {{{0},{1}}} from {2} at {3}", x, y, joueur.Nom, coup.HeureCoup));
            return coup;
        }

        public DbCoup PasserTour (DbJoueur joueur)
        {
            Debug.WriteLine(string.Format("{0} à passé son tour", joueur.Nom));
            DbCoup coup = new DbCoup
            {
                DbPartie = this,
                HeureCoup = DateTime.Now,
                IdJoueur = joueur.IdJoueur,
                X = null,
                Y = null
            };

            return coup;
        }

        public static List<DbPartie> GetAllRunningGames (DbGobansDataContext context)
        {
            var runningGames = context.DbParties.Where(g => g.EtatPartie == "playing");
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
                this.EtatPartie = "playing";
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
