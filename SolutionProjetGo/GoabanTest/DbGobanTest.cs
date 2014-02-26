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
            DbGoban goban = new DbGoban();
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
            DbPartie dummyPartie = getDummyPartie();

            DbGoban goban1 = new DbGoban {DbPartie = dummyPartie};
            DbPion p11 = goban1.PoserPion(0, 0, "Noir");
            DbPion p12 = goban1.PoserPion(1, 1, "Blanc");

            DbGoban goban2 = new DbGoban { DbPartie = dummyPartie };
            DbPion p21 = goban2.PoserPion(0, 0, "Noir");
            DbPion p22 = goban2.PoserPion(1, 1, "Blanc");

            DbGoban goban3 = new DbGoban { DbPartie = dummyPartie };
            DbPion p31 = goban3.PoserPion(0, 0, "Noir");
            DbPion p32 = goban3.PoserPion(1, 1, "Blanc");
            DbPion p33 = goban3.PoserPion(2, 2, "Noir");

            DbGoban goban4 = new DbGoban { DbPartie = dummyPartie };
            DbPion p41 = goban4.PoserPion(0, 0, "Noir");
            DbPion p42 = goban4.PoserPion(1, 1, "Blanc");

            List<DbGoban> gbs = new List<DbGoban> {goban1, goban2, goban3, goban4};
            List<DbPion> pions = new List<DbPion> { p11, p12, p21, p22, p31, p32, p33, p41, p42};

            Context.DbParties.InsertOnSubmit(dummyPartie);
            Context.DbGobans.InsertAllOnSubmit(gbs);
            Context.DbPions.InsertAllOnSubmit(pions);

            Context.SubmitChanges();

            Context.DbPions.Context.Refresh(RefreshMode.OverwriteCurrentValues, pions);

            var lastCoup = goban3.GetLastCoup();

            Context.DbParties.DeleteOnSubmit(dummyPartie);
            Context.DbJoueurs.DeleteOnSubmit(dummyPartie.DbJoueurs_IdJoueurNoir);
            Context.DbJoueurs.DeleteOnSubmit(dummyPartie.DbJoueurs_IdJoueurBlanc);

            Context.DbGobans.DeleteOnSubmit(goban1);
            Context.DbGobans.DeleteOnSubmit(goban2);
            Context.DbGobans.DeleteOnSubmit(goban3);
            Context.DbGobans.DeleteOnSubmit(goban4);

            Context.DbPions.DeleteOnSubmit(p11);
            Context.DbPions.DeleteOnSubmit(p12);
            Context.DbPions.DeleteOnSubmit(p21);
            Context.DbPions.DeleteOnSubmit(p22);
            Context.DbPions.DeleteOnSubmit(p31);
            Context.DbPions.DeleteOnSubmit(p32);
            Context.DbPions.DeleteOnSubmit(p33);
            Context.DbPions.DeleteOnSubmit(p41);
            Context.DbPions.DeleteOnSubmit(p42);

            Context.SubmitChanges();

            Assert.AreEqual(3, lastCoup.NumeroCoup);

        }

        private DbPartie getDummyPartie ()
        {
            DbJoueur noir = new DbJoueur {Nom = "Noir"};
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
