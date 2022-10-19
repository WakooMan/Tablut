using Tablut.Model.GameModel;

namespace GameModelTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMovement()
        {
            GameModel gameModel = new GameModel("Viktor", "Valaki");
            Assert.IsTrue(gameModel.Table.GetField(3, 3).Piece == null);
            gameModel.SelectPieceOrStepWithSelectedPiece(4,3);
            Assert.IsTrue(gameModel.SelectedPiece != null);
            gameModel.SelectPieceOrStepWithSelectedPiece(3,3);
            Assert.IsTrue(gameModel.Table.GetField(3,3).Piece != null);
        }
    }
}