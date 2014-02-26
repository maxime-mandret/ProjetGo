using System;
using System.Collections.Generic;
using System.Linq;
using DbGobansContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoabanTest
{
    [TestClass]
    public class DbPlayerTest : DbTestClass
    {
        [TestMethod]
        public void ConnectOrCreateTest()
        {
            // Joueur
            const string playerName = "Test Player";
            var joueur = DbJoueur.ConnectOrCreatePlayer(playerName, Context);
            Context.SubmitChanges();

            // Recuperation du joueur
            var basePlayer = Context.DbJoueurs.FirstOrDefault(player => player.Nom == playerName);

            // Suppresion des données de test
            Context.DbJoueurs.DeleteOnSubmit(joueur);
            Context.SubmitChanges();

            // Tests
            Assert.IsNotNull(joueur);
            Assert.IsNotNull(basePlayer); 
        }

        [TestMethod]
        public void GetCurrentGamesTest()
        {
            // Joueurs
            var boby = new DbJoueur { Nom = "Boby" };
            var john = new DbJoueur { Nom = "John" };
            var paul = new DbJoueur { Nom = "Paul" };
            var js = new List<DbJoueur>{boby, john, paul};

            // Parties
            var partie1 = new DbPartie {DbJoueurs_IdJoueurBlanc = boby, DbJoueurs_IdJoueurNoir = john, HeureDebut = DateTime.Now};
            var partie2 = new DbPartie { DbJoueurs_IdJoueurNoir = boby, DbJoueurs_IdJoueurBlanc = paul, HeureDebut = DateTime.Now };
            var partie3 = new DbPartie { DbJoueurs_IdJoueurBlanc = boby, DbJoueurs_IdJoueurNoir = john, HeureDebut = DateTime.Now };
            var ps = new List<DbPartie>{partie1, partie2, partie3};

            // Insertion
            Context.DbJoueurs.InsertAllOnSubmit(js);
            Context.DbParties.InsertAllOnSubmit(ps);
            Context.SubmitChanges();

            // Récuperation des parties en cours
            var bobyCount = boby.GetCurrentGames().Count;
            var johnCount = john.GetCurrentGames().Count;
            var paulCount = paul.GetCurrentGames().Count;

            // Suppression des données de test
            Context.DbParties.DeleteAllOnSubmit(ps);
            Context.DbJoueurs.DeleteAllOnSubmit(js);
            Context.SubmitChanges();

            // Tests
            Assert.IsTrue(bobyCount == 3);
            Assert.IsTrue(johnCount == 2);
            Assert.IsTrue(paulCount == 1);
        }
    }
}
