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
            DbJoueur.ConnectOrCreatePlayer("Duck My Sick", Context);
            Assert.IsTrue(Context.Connection.State == ConnectionState.Closed);
            Assert.IsTrue(Context.DbJoueurs.Count(player => player.Nom == "Duck My Sick") > 0);
            
        }
    }
}
