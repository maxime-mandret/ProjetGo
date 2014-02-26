using System;
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
            const string playerName = "Test Player";

            DbJoueur joueur = DbJoueur.ConnectOrCreatePlayer(playerName, Context);
            Context.SubmitChanges();
            Assert.IsTrue(Context.DbJoueurs.Count(player => player.Nom == playerName) > 0);
            Assert.IsNotNull(joueur);
            
            Context.DbJoueurs.DeleteOnSubmit(joueur);
            Context.SubmitChanges();
        }

        [TestMethod]
        public void GetCurrentGamesTest()
        {
            DbJoueur boby = new DbJoueur { Nom = "Boby" };
            DbJoueur john = new DbJoueur { Nom = "Boby" };
            DbJoueur paul = new DbJoueur { Nom = "Boby" };
            

            DbPartie partie1 = new DbPartie {DbJoueurs_IdJoueurBlanc = boby, DbJoueurs_IdJoueurNoir = john, HeureDebut = DateTime.Now};
            DbPartie partie2 = new DbPartie { DbJoueurs_IdJoueurNoir = boby, DbJoueurs_IdJoueurBlanc = paul, HeureDebut = DateTime.Now };
            DbPartie partie3 = new DbPartie { DbJoueurs_IdJoueurBlanc = boby, DbJoueurs_IdJoueurNoir = john, HeureDebut = DateTime.Now };
            

            Context.DbJoueurs.InsertOnSubmit(boby);
            Context.DbJoueurs.InsertOnSubmit(john);
            Context.DbJoueurs.InsertOnSubmit(paul);
            Context.DbParties.InsertOnSubmit(partie1);
            Context.DbParties.InsertOnSubmit(partie2);
            Context.DbParties.InsertOnSubmit(partie3);

            Context.SubmitChanges();

            Assert.IsTrue(boby.GetCurrentGames().Count == 3);
            Assert.IsTrue(john.GetCurrentGames().Count == 2);
            Assert.IsTrue(paul.GetCurrentGames().Count == 1);

            Context.DbParties.DeleteOnSubmit(partie1);
            Context.DbParties.DeleteOnSubmit(partie2);
            Context.DbParties.DeleteOnSubmit(partie3);
            Context.DbJoueurs.DeleteOnSubmit(boby);
            Context.DbJoueurs.DeleteOnSubmit(john);
            Context.DbJoueurs.DeleteOnSubmit(paul);

            Context.SubmitChanges();
        }
    }
}
