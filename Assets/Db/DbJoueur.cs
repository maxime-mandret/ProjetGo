﻿using System.Diagnostics;

namespace Assets.Db
{
    public class DbJoueur
    {
        public virtual int Id { get; set; }
        public virtual string Nom { get; set; }


      //`idJoueur` int(10) NOT NULL AUTO_INCREMENT,
      //`nom` varchar(20) NOT NULL,

        //public EntitySet<DbPartie> GetCurrentGames()
        //{
        //    var currentGames = new EntitySet<DbPartie>();
        //    currentGames.AddRange(DbParties_IdJoueurBlanc);
        //    currentGames.AddRange(DbParties_IdJoueurNoir);
        //    return currentGames;
        //}

        //public static DbJoueur ConnectOrCreatePlayer (string playerName, DbGobansDataContext context)
        //{
            
        //    var player = context.DbJoueurs.FirstOrDefault(p => p.Nom == playerName);
        //    if (player == null)
        //    {
        //        Debug.WriteLine("Creating player " + playerName);
        //        var newPlayer = new DbJoueur { Nom = playerName };
        //        context.DbJoueurs.InsertOnSubmit(newPlayer);
        //        return newPlayer;
        //    }
        //    else
        //    {
        //        Debug.WriteLine("Player " + playerName + " found");
        //        return player;
        //    }
        //}

        //public DbPartie CreateGame()
        //{
        //    DbPartie partie = new DbPartie {DbJoueurs_IdJoueurNoir = this};
        //    DbParties_IdJoueurNoir.Add(partie);
        //    return partie;
        //}

        //public override string ToString()
        //{
        //    return string.Format("{0} - {1}", IdJoueur, Nom);
        //}
    }
}
