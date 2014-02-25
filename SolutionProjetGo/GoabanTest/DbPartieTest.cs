using System;
using System.Diagnostics;
using DbGobansContext;
using DbObjectLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoabanTest
{
    [TestClass]
    public class DbPartieTest
    {
        [TestMethod]
        public void TestGetPendingGames()
        {
            var pending = DbGeneral.GetPendingGames();
            Debug.WriteLine(pending.Count + " games found");
            foreach (DbPartie game in pending)
            {
                Debug.WriteLine("Id Partie : " + game.IdPartie);
            }
        }
    }
}
