using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Devart.Data.Linq;
using Devart.Data.Linq.Mapping;

namespace DbGobansContext
{
    public partial class DbJoueur
    {
        public EntitySet<DbPartie> GetCurrentGames()
        {
            var currentGames = new EntitySet<DbPartie>();
            currentGames.AddRange(DbParties_IdJoueurBlanc);
            currentGames.AddRange(DbParties_IdJoueurNoir);
            return currentGames;
        }

        public static DbJoueur ConnectOrCreatePlayer (string playerName, DbGobansDataContext context)
        {
            
            var player = context.DbJoueurs.FirstOrDefault(p => p.Nom == playerName);
            if (player == null)
            {
                Debug.WriteLine("Creating player " + playerName);
                var newPlayer = new DbJoueur { Nom = playerName };
                context.DbJoueurs.InsertOnSubmit(newPlayer);
                return newPlayer;
            }
            else
            {
                Debug.WriteLine("Player " + playerName + " found");
                return player;
            }
        }

        public DbPartie CreateGame()
        {
            DbPartie partie = new DbPartie {DbJoueurs_IdJoueurNoir = this};
            DbParties_IdJoueurNoir.Add(partie);
            return partie;
        }
    }
}
