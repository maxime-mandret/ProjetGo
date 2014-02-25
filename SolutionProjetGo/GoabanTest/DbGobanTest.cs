using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbGobansContext;
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
    }
}
