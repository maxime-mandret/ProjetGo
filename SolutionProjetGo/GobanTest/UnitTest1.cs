using System;
using Assets.GameLogic;
using Assets.ObjetsDeJeu;
using Assets.Db;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GobanTest
{
    [TestClass]
    public class TestRemoteMoveStalker : DbTestClass
    {
        [TestMethod]
        public void TestMethod1 ()
        {
            Player j1 = new Player("noir", PlayerColor.Black);
            Player j2 = new RemotePlayer("blanc", PlayerColor.White);

            DbPartie p = new DbPartie
            {
                DbJoueurs_IdJoueurNoir = DbJoueur.ConnectOrCreatePlayer(j1.Name, this.Context),
                DbJoueurs_IdJoueurBlanc = DbJoueur.ConnectOrCreatePlayer(j2.Name, this.Context),
                HeureDebut = DateTime.Now
            };
            //Context.DbJoueurs.InsertOnSubmit(j1);
            //Context.DbJoueurs.InsertOnSubmit(j2);
            Context.DbParties.InsertOnSubmit(p);
            Context.SubmitChanges();

            RemoteGame game = new RemoteGame(p);

            Assert.AreEqual(game.DbBlackPlayer, game.CurrentDbPlayer);
            Assert.AreEqual(game.BlackPlayer, game.CurrentPlayer);

            // Le joueur noir joue
            game.PutRock(0, 0);

            Assert.AreEqual(game.DbWhitePlayer, game.CurrentDbPlayer);
            Assert.AreEqual(game.WhitePlayer, game.CurrentPlayer);

            // Insertion en base (sans passer la logique de jeu)
            // Le joueur blanc distant joue
            game.DbPartie.PoserPion(1, 1, game.CurrentDbPlayer);

            System.Threading.Thread.Sleep(1001);

            
            Assert.AreEqual(game.DbBlackPlayer, game.CurrentDbPlayer);
            Assert.IsNotNull(game.Goban[1,1].Owner);
        }
    }
}
