using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tablut.Model.GameModel;

namespace GameModelTests
{
    [TestClass]
    public class TestSelect
    {
        [TestMethod]
        public void TestSelectChange()
        {
            GameModel gameModel = new GameModel("Viktor", "Valaki");
            Assert.IsTrue(gameModel.Table.GetField(4, 2).Piece != null && gameModel.Table.GetField(4, 3).Piece != null);
            gameModel.StepOrSelect(4, 2);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 4 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 2 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
            gameModel.StepOrSelect(4, 3);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 4 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 3 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
        }

        [TestMethod]
        public void TestSelectOnlyYourOwnPiece()
        {
            GameModel gameModel = new GameModel("Viktor", "Valaki");
            Assert.IsTrue(gameModel.Table.GetField(4, 2).Piece != null && gameModel.Table.GetField(4, 0).Piece != null && gameModel.Table.GetField(4, 2).Piece.Player == gameModel.CurrentPlayer && gameModel.Table.GetField(4, 0).Piece.Player != gameModel.CurrentPlayer);
            gameModel.StepOrSelect(4, 0);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece == null);
            gameModel.StepOrSelect(4, 2);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 4 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 2 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
        }

        [TestMethod]
        public void TestSelectingEmptyField()
        {
            GameModel gameModel = new GameModel("Viktor", "Valaki");
            Assert.IsTrue(gameModel.Table.GetField(3, 3).Piece == null);
            gameModel.StepOrSelect(3, 3);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece == null );
        }
    }
}
