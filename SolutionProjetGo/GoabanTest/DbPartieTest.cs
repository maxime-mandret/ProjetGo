using System;
using System.Diagnostics;
using DbGobansContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoabanTest
{
    [TestClass]
    public class DbPartieTest : DbTestClass
    {
        [TestMethod]
        public void TestGetPendingGames()
        {

            DbPartie p1 = getDummyPartie();
            p1.DbJoueurs_IdJoueurBlanc = null;
            DbPartie p2 = getDummyPartie();
            DbPartie p3 = getDummyPartie();
            DbPartie p4 = getDummyPartie();
            p4.DbJoueurs_IdJoueurBlanc = null;

            Context.DbParties.InsertOnSubmit(p1);
            Context.DbParties.InsertOnSubmit(p2);
            Context.DbParties.InsertOnSubmit(p3);
            Context.DbParties.InsertOnSubmit(p4);

            Context.SubmitChanges();

            var pending = DbPartie.GetPendingGames(Context);

            Context.DbParties.DeleteOnSubmit(p1);
            Context.DbParties.DeleteOnSubmit(p2);
            Context.DbParties.DeleteOnSubmit(p3);
            Context.DbParties.DeleteOnSubmit(p4);

            Context.DbJoueurs.DeleteOnSubmit(p2.DbJoueurs_IdJoueurBlanc);
            Context.DbJoueurs.DeleteOnSubmit(p2.DbJoueurs_IdJoueurNoir);

            Context.SubmitChanges();

            Assert.AreEqual(2, pending.Count);
        }

        [TestMethod]
        public void TestAddPlayer ()
        {
            DbJoueur j1 = new DbJoueur {Nom = "J1"};
            DbJoueur j2 = new DbJoueur {Nom = "J2"};
            DbPartie partie = new DbPartie();
            partie.AddPlayer(j1);
            partie.AddPlayer(j2);
            Assert.AreEqual(j1, partie.DbJoueurs_IdJoueurNoir);
            Assert.AreEqual(j2, partie.DbJoueurs_IdJoueurBlanc);
            Assert.AreEqual("En cours", partie.EtatPartie);
            try
            {
                partie.AddPlayer(j1);
                Assert.Fail("No exception for full game");
            }
            catch (InvalidOperationException e)
            {
                Assert.AreEqual("Game is full", e.Message);
            }
            catch (Exception e)
            {
                Assert.Fail("Unexpected exception");
                throw;
            }
        }

        [TestMethod]
        public void TestGetRunningGames ()
        {
            DbJoueur j1 = new DbJoueur { Nom = "J1" };
            DbJoueur j2 = new DbJoueur { Nom = "J2" };
            

            DbPartie partie1 = new DbPartie();
            partie1.AddPlayer(j1);
            partie1.AddPlayer(j2);

            DbPartie partie2 = new DbPartie();
            partie2.AddPlayer(j1);
            partie2.AddPlayer(j2);

            DbPartie partie3 = new DbPartie();
            partie3.AddPlayer(j1);
            partie3.AddPlayer(j2); 

            DbPartie partie4 = new DbPartie();
            partie4.AddPlayer(j1);
            partie4.AddPlayer(j2);

            Context.DbJoueurs.InsertOnSubmit(j1);
            Context.DbJoueurs.InsertOnSubmit(j2);

            Context.DbParties.InsertOnSubmit(partie1);
            Context.DbParties.InsertOnSubmit(partie2);
            Context.DbParties.InsertOnSubmit(partie3);
            Context.DbParties.InsertOnSubmit(partie4);

            Context.SubmitChanges();

            var runningGames = DbPartie.GetAllRunningGames(Context);
            Assert.AreEqual(4, runningGames.Count);
            Debug.WriteLine(runningGames.Count + " running games found");
            foreach (DbPartie game in runningGames)
            {
                Debug.WriteLine("Id Partie : " + game.IdPartie);
            }
            Context.DbJoueurs.DeleteOnSubmit(j1);
            Context.DbJoueurs.DeleteOnSubmit(j2);

            Context.DbParties.DeleteOnSubmit(partie1);
            Context.DbParties.DeleteOnSubmit(partie2);
            Context.DbParties.DeleteOnSubmit(partie3);
            Context.DbParties.DeleteOnSubmit(partie4);

            Context.SubmitChanges();
        }

        private DbPartie getDummyPartie ()
        {
            DbJoueur noir = new DbJoueur { Nom = "Noir" };
            DbJoueur blanc = new DbJoueur { Nom = "Blanc" };
            DbPartie partie = new DbPartie
            {
                DbJoueurs_IdJoueurNoir = noir,
                DbJoueurs_IdJoueurBlanc = blanc,
            };

            return partie;
        }
    }
}
