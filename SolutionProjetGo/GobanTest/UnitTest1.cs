using System;
using Assets.GameLogic;
using Assets.ObjetsDeJeu;
using DbGobansContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GobanTest
{
    [TestClass]
    public class TestRemoteMoveStalker
    {
        [TestMethod]
        public void TestMethod1 ()
        {
            Player j1 = new Player("noir", PlayerColor.Black);
            Player j2 = new RemotePlayer("blanc", PlayerColor.White);

            RemoteGame game = new RemoteGame(9, j1, j2);

            Assert.AreEqual(game.DbBlackPlayer, game.CurrentDbPlayer);
            Assert.AreEqual(game.BlackPlayer, game.CurrentPlayer);

            // Le joueur noir joue
            game.PutRock(0, 0);

            Assert.AreEqual(game.DbWhitePlayer, game.CurrentDbPlayer);
            Assert.AreEqual(game.WhitePlayer, game.CurrentPlayer);

            // Insertion en base (sans passer la logique de jeu)
            // Le joueur blanc distant joue
            game.DbPartie.PoserPion(1, 1, game.CurrentDbPlayer);
            game.ApplicationDataContext.SubmitChanges();

            System.Threading.Thread.Sleep(1001);

            // Attente du timeout du stalker
            //while (game.CurrentPlayer != game.BlackPlayer)
            //{
            //    game.Update();
            //}

            
            Assert.AreEqual(game.DbBlackPlayer, game.CurrentDbPlayer);
            Assert.IsNotNull(game.Goban[1,1].Owner);
        }
    }
}
