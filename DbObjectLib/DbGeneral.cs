using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DbGobansContext;
using Devart.Data.Linq;

namespace DbObjectLib
{
    public static class DbGeneral
    {

        public static List<DbPartie> GetPendingGames()
        {
            var incompleteGames = new List<DbPartie>();
            using (var context = new DbGobansDataContext())
            {
                incompleteGames.AddRange(context.DbParties.Where(part => part.DbJoueurs_IdJoueurNoir == null || part.DbJoueurs_IdJoueurBlanc == null));
                Debug.WriteLine(incompleteGames.Count + " pending games found");
            }
            return incompleteGames;
        }

        public static int ConnectToPartie(long gameId)
        {
            DbPartie partie = null;
            using (var context = new DbGobansDataContext())
            {
                partie = context.DbParties.FirstOrDefault(part => part.IdPartie == gameId);
                Debug.WriteLine("Connection to partie id : " + partie.IdPartie);
                Debug.WriteLine("Nombre de joueur : " + partie.GetNombreJoueurs());
            }
            return partie.GetNombreJoueurs();
        }
    }
}
