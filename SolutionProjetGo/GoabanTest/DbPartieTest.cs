using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using DbGobansContext;
using Devart.Data.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoabanTest
{
    [TestClass]
    public class DbPartieTest : DbTestClass
    {
        [TestMethod]
        public void TestGetPendingGames()
        {
            // Joueurs
            var noir = new DbJoueur { Nom = "Noir" };
            var blanc = new DbJoueur { Nom = "Blanc" };

            // Parties
            var p1 = new DbPartie { DbJoueurs_IdJoueurNoir = noir };
            var p2 = new DbPartie { DbJoueurs_IdJoueurNoir = noir };
            var p3 = new DbPartie { DbJoueurs_IdJoueurNoir = noir, DbJoueurs_IdJoueurBlanc = blanc };
            var p4 = new DbPartie { DbJoueurs_IdJoueurNoir = noir };
            var parties = new List<DbPartie> { p1, p2, p3, p4 };
            
            //Insertion en base
            Context.DbParties.InsertAllOnSubmit(parties);
            Context.SubmitChanges();

            // Récupération des parties en attente de joueurs
            var pending = DbPartie.GetPendingGames(Context);

            // Suppression
            Context.DbParties.DeleteAllOnSubmit(parties);
            Context.DbJoueurs.DeleteOnSubmit(noir);
            Context.DbJoueurs.DeleteOnSubmit(blanc);

            Context.SubmitChanges();

            // Tests
            Assert.AreEqual(3, pending.Count);
        }

        [TestMethod]
        public void TestAddPlayer ()
        {
            var j1 = new DbJoueur {Nom = "J1"};
            var j2 = new DbJoueur {Nom = "J2"};
            var partie = new DbPartie();
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
            var j1 = new DbJoueur { Nom = "J1" };
            var j2 = new DbJoueur { Nom = "J2" };
            

            var partie1 = new DbPartie();
            partie1.AddPlayer(j1);
            partie1.AddPlayer(j2);

            var partie2 = new DbPartie();
            partie2.AddPlayer(j1);

            var partie3 = new DbPartie();
            partie3.AddPlayer(j1);
            partie3.AddPlayer(j2); 

            var partie4 = new DbPartie();
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
            
            
            Context.DbJoueurs.DeleteOnSubmit(j1);
            Context.DbJoueurs.DeleteOnSubmit(j2);

            Context.DbParties.DeleteOnSubmit(partie1);
            Context.DbParties.DeleteOnSubmit(partie2);
            Context.DbParties.DeleteOnSubmit(partie3);
            Context.DbParties.DeleteOnSubmit(partie4);

            Context.SubmitChanges();

            Assert.AreEqual(3, runningGames.Count);
        }

        [TestMethod]
        public void PoserPionTest ()
        {
            var partie = new DbPartie();
            var j1 = new DbJoueur { Nom = "J1" };
            var j2 = new DbJoueur { Nom = "J2" };

            partie.PoserPion(0, 0, j1);
            partie.PoserPion(1, 1, j2);

            var expected1 = partie.DbCoups.FirstOrDefault(coup => coup.X == 0 && coup.Y == 0);
            Assert.AreEqual(j1.IdJoueur, expected1.IdJoueur);

            var expected2 = partie.DbCoups.FirstOrDefault(coup => coup.X == 1 && coup.Y == 1);
            Assert.AreEqual(j2.IdJoueur, expected2.IdJoueur);
        }

        [TestMethod]
        public void GetLastCoupTest ()
        {
            // Joueurs
            var noir = new DbJoueur { Nom = "Noir" };
            var blanc = new DbJoueur { Nom = "Blanc" };


            // Gobans
            var partie = new DbPartie
            {
                DbJoueurs_IdJoueurNoir = noir,
                DbJoueurs_IdJoueurBlanc = blanc,
                HeureDebut = DateTime.Now,
            };

            var p0 = partie.PoserPion(0, 0, noir);
            System.Threading.Thread.Sleep(1000);
            var p1 = partie.PoserPion(1, 1, blanc);
            System.Threading.Thread.Sleep(1000);
            var p2 = partie.PoserPion(2, 2, noir);
            System.Threading.Thread.Sleep(1000);
            var p3 = partie.PoserPion(3, 3, blanc);

            

            var lastCoup = partie.GetLastCoup();

            // Test
            Assert.AreEqual(p3, lastCoup);

        }
    }
}
