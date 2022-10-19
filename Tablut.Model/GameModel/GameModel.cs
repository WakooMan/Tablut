using System.ComponentModel.DataAnnotations.Schema;

namespace Tablut.Model.GameModel
{
    public class GameModel
    {
        public EventHandler OnKingDies, OnWrongStep, OnSoldierDies, OnDefenderWins, OnAttackerWins, OnPieceSteps, OnPieceSelected, OnPlayerTurnChange;

        private Piece? selectedPiece;
        private Player currentPlayer;

        public Piece? SelectedPiece => selectedPiece;
        public Player CurrentPlayer => currentPlayer;

        private Player[] players;
        private Table table;


        public GameModel(string AttackerName, string DefenderName)
        {
            selectedPiece = null;
            table = new Table();
            players = new Player[2];
            players[0] = new Player(AttackerName, PlayerSide.Attacker, table);
            players[1] = new Player(DefenderName, PlayerSide.Defender, table);
            currentPlayer = players[1];
        }

        public void SelectPieceOrStepWithSelectedPiece(int x, int y)
        { 

        }

        private void SelectPiece(int x, int y)
        { 
        }

        private void StepWithSelectedPiece(int x, int y)
        {
            
        }

    }
}