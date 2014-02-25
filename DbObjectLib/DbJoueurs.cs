using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devart.Data.Linq;
using Devart.Data.Linq.Mapping;

namespace DbGobansContext
{
    public partial class DbJoueur
    {
        public bool IsConnectedToParty()
        {
            return this.DbParties_IdJoueurBlanc.Count + this.DbParties_IdJoueurNoir.Count > 0;
        }

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

                //context.Connection.Open();
                //var transaction = context.Connection.BeginTransaction(IsolationLevel.Serializable);

                //Debug.WriteLine("Creating player " + playerName);
                //var newPlayer = new DbJoueurs { Nom = playerName };
                //context.DbJoueurs.InsertOnSubmit(newPlayer);

                //transaction.Commit();
                //context.Connection.Close();

                Debug.WriteLine("Creating player " + playerName);
                var newPlayer = new DbJoueur { Nom = playerName };
                context.DbJoueurs.InsertOnSubmit(newPlayer);
                context.SubmitChanges();

                return newPlayer;
            }
            else
            {
                Debug.WriteLine("Player " + playerName + " found");
                return player;
            }
        }
    }
}
