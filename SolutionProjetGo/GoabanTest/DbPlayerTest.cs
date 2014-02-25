using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            DbJoueur joueur = null;
            try
            {
                joueur = DbJoueur.ConnectOrCreatePlayer(playerName, Context);
                Context.SubmitChanges();
                Assert.IsTrue(Context.DbJoueurs.Count(player => player.Nom == playerName) > 0);
                Assert.IsNotNull(joueur);
            }
            catch (Exception e)
            {
                if (joueur != null)
                {
                    Context.DbJoueurs.DeleteOnSubmit(joueur);
                    Context.SubmitChanges();
                }
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetCurrentGamesTest()
        {
            DbJoueur boby = new DbJoueur {Nom = "Boby"};
            Context.DbJoueurs.InsertOnSubmit(boby);

            DbPartie partie1 = new DbPartie {DbJoueurs_IdJoueurBlanc = boby, HeureDebut = DateTime.Now};
            Context.DbParties.InsertOnSubmit(partie1);

            DbPartie partie2 = new DbPartie { DbJoueurs_IdJoueurNoir = boby, HeureDebut = DateTime.Now };
            Context.DbParties.InsertOnSubmit(partie2);

            DbPartie partie3 = new DbPartie { DbJoueurs_IdJoueurBlanc = boby, HeureDebut = DateTime.Now };
            Context.DbParties.InsertOnSubmit(partie3);
            Context.SubmitChanges();

            var currentGames = boby.GetCurrentGames();
            Assert.IsTrue(currentGames.Count == 3);

            Context.DbParties.DeleteOnSubmit(partie1);
            Context.DbParties.DeleteOnSubmit(partie2);
            Context.DbParties.DeleteOnSubmit(partie2);
            Context.DbJoueurs.DeleteOnSubmit(boby);
        }
    }
}
