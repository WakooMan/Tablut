using Tablut.Model.GameModel;

namespace GameModelTests
{
    [TestClass]
    public class TestMovement
    {
        [TestMethod]
        public void TestBasicMovement()
        {
            GameModel gameModel = new GameModel("Viktor", "Valaki");
            Assert.IsTrue(gameModel.Table.GetField(3, 3).Piece == null);
            gameModel.SelectOrStep(4,3);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 4 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 3 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(3,3);
            Assert.IsTrue(gameModel.Table.GetField(3,3).Piece != null);
        }

        [TestMethod]
        public void TestKilling()
        {
            GameModel gameModel = new GameModel("Viktor", "Valaki", new (int x, int y)[16] { (0, 3), (0, 4), (0, 5), (1, 4), (3, 2), (4, 0), (5, 0), (4, 1), (3, 8), (4, 8), (5, 8), (4, 7), (8, 3), (8, 4), (8, 5), (7, 4) }, new (int x, int y)[9] { (4, 4), (4, 5), (4, 3), (3, 4), (5, 4), (2, 4), (4, 2), (6, 4), (4, 6) },PlayerSide.Defender);
            Assert.IsTrue(gameModel.Table.GetField(3, 2).Piece != null && gameModel.Table.GetField(4, 2).Piece != null && gameModel.Table.GetField(2, 4).Piece != null && gameModel.Table.GetField(2, 2).Piece == null && gameModel.Table.GetField(2, 4).Piece.Player == gameModel.CurrentPlayer && gameModel.Table.GetField(4, 2).Piece.Player == gameModel.CurrentPlayer && gameModel.Table.GetField(3, 2).Piece.Player != gameModel.CurrentPlayer);
            gameModel.SelectOrStep(2, 4);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 2 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 4 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(2, 2);
            Assert.IsTrue(gameModel.Table.GetField(2, 2).Piece != null && gameModel.Table.GetField(3, 2).Piece == null);
        }

        [TestMethod]
        public void TestMovementThroughPiece()
        {
            GameModel gameModel = new GameModel("Viktor", "Valaki", new (int x, int y)[16] { (0, 3), (0, 4), (0, 5), (1, 4), (3, 2), (4, 0), (5, 0), (4, 1), (3, 8), (4, 8), (5, 8), (4, 7), (8, 3), (8, 4), (8, 5), (7, 4) }, new (int x, int y)[9] { (4, 4), (4, 5), (4, 3), (3, 4), (5, 4), (2, 4), (4, 2), (6, 4), (4, 6) }, PlayerSide.Defender);
            Assert.IsTrue(gameModel.Table.GetField(3, 2).Piece != null && gameModel.Table.GetField(4, 2).Piece != null && gameModel.Table.GetField(2, 2).Piece == null && gameModel.Table.GetField(4, 2).Piece.Player == gameModel.CurrentPlayer && gameModel.Table.GetField(3, 2).Piece.Player != gameModel.CurrentPlayer);
            gameModel.SelectOrStep(4, 2);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 4 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 2 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(2, 2);
            Assert.IsTrue(gameModel.Table.GetField(2, 2).Piece == null && gameModel.Table.GetField(3, 2).Piece != null && gameModel.Table.GetField(4, 2).Piece != null);
        }

        [TestMethod]
        public void TestWrongMovement()
        {
            GameModel gameModel = new GameModel("Viktor", "Valaki");
            Assert.IsTrue(gameModel.Table.GetField(4, 2).Piece != null && gameModel.Table.GetField(3, 3).Piece == null && gameModel.Table.GetField(4, 2).Piece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(4, 2);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 4 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 2 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(3, 3);
            Assert.IsTrue(gameModel.Table.GetField(4, 2).Piece != null && gameModel.Table.GetField(3, 3).Piece == null);
        }

        [TestMethod]
        public void TestMovementToMiddleField()
        {
            GameModel gameModel = new GameModel("Viktor", "Valaki", new (int x, int y)[16] { (0, 3), (0, 4), (0, 5), (1, 4), (3, 2), (4, 0), (5, 0), (4, 1), (3, 8), (4, 8), (5, 8), (4, 7), (8, 3), (8, 4), (8, 5), (7, 4) }, new (int x, int y)[9] { (3, 3), (4, 5), (4, 3), (3, 4), (5, 4), (2, 4), (4, 2), (6, 4), (4, 6) }, PlayerSide.Defender);
            Assert.IsTrue(gameModel.Table.GetField(4, 3).Piece != null && gameModel.Table.GetField(4, 4).Piece == null && gameModel.Table.GetField(4,3).Piece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(4, 3);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 4 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 3 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(4, 4);
            Assert.IsTrue(gameModel.Table.GetField(4, 3).Piece != null && gameModel.Table.GetField(4, 4).Piece == null);
        }


        [TestMethod]
        public void TestKillingTheKing()
        {
            bool AttackerWon = false;
            GameModel gameModel = new GameModel("Viktor", "Valaki", new (int x, int y)[16] { (0, 3), (0, 4), (0, 5), (1, 4), (2, 0), (4, 0), (5, 0), (4, 1), (3, 8), (4, 8), (5, 8), (4, 7), (8, 3), (8, 4), (8, 5), (7, 4) }, new (int x, int y)[9] { (1, 3), (4, 5), (4, 3), (3, 4), (5, 4), (2, 4), (4, 2), (6, 4), (4, 6) },PlayerSide.Attacker);
            gameModel.OnAttackerWinsEvent += (o, e) =>
            {
                AttackerWon = true;
            };
            Assert.IsTrue(gameModel.Table.GetField(2, 0).Piece != null && gameModel.Table.GetField(1, 3).Piece != null && gameModel.Table.GetField(2, 3).Piece == null && gameModel.Table.GetField(2, 0).Piece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(2, 0);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 2 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 0 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(2, 3);
            Assert.IsTrue(gameModel.Table.GetField(2, 3).Piece != null && gameModel.Table.GetField(1, 3).Piece == null && gameModel.Table.GetField(0, 3).Piece != null && AttackerWon && gameModel.GameState == GameState.GameOver);
        }

        [TestMethod]
        public void TestKingCantKill()
        {
            GameModel gameModel = new GameModel("Viktor", "Valaki", new (int x, int y)[2] { (0,0),(2,4) }, new (int x, int y)[2] { (4, 4), (1, 4)}, PlayerSide.Defender);
            Assert.IsTrue(gameModel.Table.GetField(4, 4).Piece != null && gameModel.Table.GetField(1, 4).Piece != null && gameModel.Table.GetField(2, 4).Piece != null && gameModel.Table.GetField(4, 4).Piece.Player == gameModel.CurrentPlayer && gameModel.Table.GetField(1, 4).Piece.Player == gameModel.CurrentPlayer && gameModel.Table.GetField(2, 4).Piece.Player != gameModel.CurrentPlayer);
            gameModel.SelectOrStep(4, 4);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 4 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 4 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(3, 4);
            Assert.IsTrue(gameModel.Table.GetField(3, 4).Piece != null && gameModel.Table.GetField(2, 4).Piece != null && gameModel.Table.GetField(1, 4).Piece != null && gameModel.Table.GetField(3, 4).Piece.Player != gameModel.CurrentPlayer && gameModel.Table.GetField(1, 4).Piece.Player != gameModel.CurrentPlayer && gameModel.Table.GetField(2, 4).Piece.Player == gameModel.CurrentPlayer);
        }

        [TestMethod]
        public void TestKingEscape()
        {
            bool DefenderWon = false;
            GameModel gameModel = new GameModel("Viktor", "Valaki", new (int x, int y)[16] { (0, 2), (0, 4), (0, 5), (1, 4), (3, 2), (4, 0), (5, 0), (4, 1), (3, 8), (4, 8), (5, 8), (4, 7), (8, 3), (8, 4), (8, 5), (7, 4) }, new (int x, int y)[9] { (1, 3), (4, 5), (4, 3), (3, 4), (5, 4), (2, 4), (4, 2), (6, 4), (4, 6) }, PlayerSide.Defender);
            gameModel.OnDefenderWinsEvent += (o, e) =>
            {
                DefenderWon = true;
            };
            Assert.IsTrue(gameModel.Table.GetField(1, 3).Piece != null && gameModel.Table.GetField(1, 3).Piece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(1, 3);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 1 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 3 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(1, 0);
            Assert.IsTrue(gameModel.Table.GetField(1, 0).Piece != null && DefenderWon && gameModel.GameState == GameState.GameOver);
        }

        [TestMethod]
        public void TestKillingWithMiddleField()
        {
            GameModel gameModel = new GameModel("Viktor", "Valaki", new (int x, int y)[16] { (0, 2), (0, 4), (0, 5), (1, 4), (3, 1), (4, 0), (5, 0), (4, 1), (3, 8), (4, 8), (5, 8), (4, 7), (8, 3), (8, 4), (8, 5), (7, 4) }, new (int x, int y)[9] { (3, 3), (4, 5), (4, 3), (3, 4), (5, 4), (2, 4), (5, 2), (6, 4), (4, 6) }, PlayerSide.Attacker);
            Assert.IsTrue(gameModel.Table.GetField(0, 2).Piece != null && gameModel.Table.GetField(4, 2).Piece == null && gameModel.Table.GetField(4, 3).Piece != null && gameModel.Table.GetField(4, 4).Piece == null && gameModel.Table.GetField(0, 2).Piece.Player == gameModel.CurrentPlayer && gameModel.Table.GetField(4, 3).Piece.Player != gameModel.CurrentPlayer);
            gameModel.SelectOrStep(0, 2);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 0 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 2 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(4, 2);
            Assert.IsTrue(gameModel.Table.GetField(4, 2).Piece != null && gameModel.Table.GetField(4, 3).Piece == null);
        }

        [TestMethod]
        public void TestKillingNotWorkingWithMiddleFieldWithKingOnMiddle()
        {
            GameModel gameModel = new GameModel("Viktor", "Valaki", new (int x, int y)[16] { (0, 2), (0, 4), (0, 5), (1, 4), (3, 1), (4, 0), (5, 0), (4, 1), (3, 8), (4, 8), (5, 8), (4, 7), (8, 3), (8, 4), (8, 5), (7, 4) }, new (int x, int y)[9] { (4, 4), (4, 5), (4, 3), (3, 4), (5, 4), (2, 4), (5, 2), (6, 4), (4, 6) }, PlayerSide.Attacker);
            Assert.IsTrue(gameModel.Table.GetField(0, 2).Piece != null && gameModel.Table.GetField(4, 2).Piece == null && gameModel.Table.GetField(4, 3).Piece != null && gameModel.Table.GetField(4, 4).Piece != null && gameModel.Table.GetField(0, 2).Piece.Player == gameModel.CurrentPlayer && gameModel.Table.GetField(4, 3).Piece.Player != gameModel.CurrentPlayer);
            gameModel.SelectOrStep(0, 2);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 0 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 2 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(4, 2);
            Assert.IsTrue(gameModel.Table.GetField(4, 2).Piece != null && gameModel.Table.GetField(4, 3).Piece != null);
        }

        [TestMethod]
        public void TestKillingLastDefender()
        {
            GameModel gameModel = new GameModel("Viktor", "Valaki",new (int x, int y)[1] { (2,3)}, new (int x, int y)[1] { (3,4) },PlayerSide.Attacker);
            Assert.IsTrue(gameModel.Table.GetField(2, 4).Piece == null && gameModel.Table.GetField(2, 3).Piece != null && gameModel.Table.GetField(3, 4).Piece != null && gameModel.Table.GetField(4, 4).Piece == null);
            gameModel.SelectOrStep(2, 3);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 2 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 3 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(2, 4);
            Assert.IsTrue(gameModel.Table.GetField(3, 4).Piece == null && gameModel.GameState == GameState.GameOver);
        }

        [TestMethod]
        public void TestKillingLastAttacker()
        {
            GameModel gameModel = new GameModel("Viktor", "Valaki", new (int x, int y)[1] { (3, 4) }, new (int x, int y)[2] { (1,1),(2, 3) }, PlayerSide.Defender);
            Assert.IsTrue(gameModel.Table.GetField(2, 4).Piece == null && gameModel.Table.GetField(2, 3).Piece != null && gameModel.Table.GetField(3, 4).Piece != null && gameModel.Table.GetField(4, 4).Piece == null);
            gameModel.SelectOrStep(2, 3);
            Assert.IsTrue(gameModel.CurrentPlayer.SelectedPiece != null && gameModel.CurrentPlayer.SelectedPiece.Place.X == 2 && gameModel.CurrentPlayer.SelectedPiece.Place.Y == 3 && gameModel.CurrentPlayer.SelectedPiece.Player == gameModel.CurrentPlayer);
            gameModel.SelectOrStep(2, 4);
            Assert.IsTrue(gameModel.Table.GetField(3, 4).Piece == null && gameModel.GameState == GameState.GameOver);
        }

    }
}