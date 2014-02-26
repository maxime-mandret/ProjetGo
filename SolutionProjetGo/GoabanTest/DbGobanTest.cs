using System.Collections.Generic;
using System.Linq;
using DbGobansContext;
using Devart.Data.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoabanTest
{
    [TestClass]
    public class DbGobanTest : DbTestClass
    {
        [TestMethod]
        public void PoserPionTest()
        {
            var goban = new DbGoban();
            goban.PoserPion(0, 0, "Noir");
            goban.PoserPion(1, 1, "Blanc");


            var expected1 = goban.DbPions.FirstOrDefault(p => p.PositionX == 0 && p.PositionY == 0);
            Assert.AreEqual("Noir", expected1.Pioncol);

            var expected2 = goban.DbPions.FirstOrDefault(p => p.PositionX == 1 && p.PositionY == 1);
            Assert.AreEqual("Blanc", expected2.Pioncol);
        }


        [TestMethod]
        public void GetLastCoupTest ()
        {
            // Joueurs
            var noir = new DbJoueur { Nom = "Noir" };
            var blanc = new DbJoueur { Nom = "Blanc" };

            // Partie
            var partie = new DbPartie
            {
                DbJoueurs_IdJoueurNoir = noir,
                DbJoueurs_IdJoueurBlanc = blanc,
            };

            // Gobans
            var goban1 = new DbGoban {DbPartie = partie};
            var p11 = goban1.PoserPion(0, 0, "Noir");
            var p12 = goban1.PoserPion(1, 1, "Blanc");

            var goban2 = new DbGoban { DbPartie = partie };
            var p21 = goban2.PoserPion(0, 0, "Noir");
            var p22 = goban2.PoserPion(1, 1, "Blanc");

            var goban3 = new DbGoban { DbPartie = partie };
            var p31 = goban3.PoserPion(0, 0, "Noir");
            var p32 = goban3.PoserPion(1, 1, "Blanc");
            var p33 = goban3.PoserPion(2, 2, "Noir");

            var goban4 = new DbGoban { DbPartie = partie };
            var p41 = goban4.PoserPion(0, 0, "Noir");
            var p42 = goban4.PoserPion(1, 1, "Blanc");

            var gbs = new List<DbGoban> {goban1, goban2, goban3, goban4};
            var pions = new List<DbPion> { p11, p12, p21, p22, p31, p32, p33, p41, p42};

            // Insertion en base
            Context.DbJoueurs.InsertOnSubmit(noir);
            Context.DbJoueurs.InsertOnSubmit(blanc);
            Context.DbParties.InsertOnSubmit(partie);
            Context.DbGobans.InsertAllOnSubmit(gbs);
            Context.DbPions.InsertAllOnSubmit(pions);

            Context.SubmitChanges();

            // Rafraichissement pour le trigger
            Context.DbPions.Context.Refresh(RefreshMode.OverwriteCurrentValues, pions);

            // Dernier coup joué
            var lastCoup = goban3.GetLastCoup();

            Context.DbPions.DeleteAllOnSubmit(pions);
            Context.DbGobans.DeleteAllOnSubmit(gbs);
            Context.DbParties.DeleteOnSubmit(partie);
            Context.DbJoueurs.DeleteOnSubmit(noir);
            Context.DbJoueurs.DeleteOnSubmit(blanc);

            Context.SubmitChanges();

            // Test
            Assert.AreEqual(3, lastCoup.NumeroCoup);

        }

    }
}
