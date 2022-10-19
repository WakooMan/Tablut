using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Tablut.Model.GameModel
{
    public class GameModel
    {
        public EventHandler? OnKingDies, OnWrongStep, OnSoldierDies, OnDefenderWins, OnAttackerWins, OnPieceSteps, OnPieceSelected, OnPlayerTurnChange;

        private Piece? selectedPiece;
        private Player currentPlayer;
        private Player[] players;
        private Table table;

        public Piece? SelectedPiece => selectedPiece;
        public Player CurrentPlayer => currentPlayer;
        public Table Table => table;

        public GameModel(string AttackerName, string DefenderName)
        {
            
            selectedPiece = null;
            table = new Table();
            players = new Player[2];
            players[0] = new Player(AttackerName, PlayerSide.Attacker, table,new (int x, int y)[16] { (0,3),(0,4),(0,5),(1,4),(3,0),(4,0),(5,0),(4,1),(3,8),(4,8),(5,8),(4,7),(8,3),(8,4),(8,5),(7,4)},OnPieceSteps,OnWrongStep,OnKingDies,OnDefenderWins,OnSoldierDies);
            players[1] = new Player(DefenderName, PlayerSide.Defender, table, new (int x, int y)[9] { (4,4), (4,5), (4,3), (3,4), (5,4), (2,4), (4,2), (6,4), (4,6) }, OnPieceSteps, OnWrongStep, OnKingDies, OnDefenderWins, OnSoldierDies);
            currentPlayer = players[1];
        }

        public GameModel(string AttackerName, string DefenderName,(int x,int y)[] AttackerValues, (int x, int y)[] DefenderValues)
        {

            selectedPiece = null;
            table = new Table();
            players = new Player[2];
            players[0] = new Player(AttackerName, PlayerSide.Attacker, table, AttackerValues, OnPieceSteps, OnWrongStep, OnKingDies, OnDefenderWins, OnSoldierDies);
            players[1] = new Player(DefenderName, PlayerSide.Defender, table, DefenderValues, OnPieceSteps, OnWrongStep, OnKingDies, OnDefenderWins, OnSoldierDies);
            currentPlayer = players[1];
        }

        public void SelectPieceOrStepWithSelectedPiece(int x, int y)
        {
            Piece? piece = currentPlayer.AlivePieces.Where(p => p.Place.X == x && p.Place.Y == y).SingleOrDefault();
            if (piece != null)
            {
                selectedPiece = piece;
            }
            else if (selectedPiece != null)
            {
                selectedPiece.TryStepToPlace(x, y);
            }
        }
    }
}