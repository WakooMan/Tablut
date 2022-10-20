using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Tablut.Model.GameModel
{
    public enum GameState
    {
        Playing=0,Paused=1,GameOver=2
    }

    public enum EventTypeFlag : uint
    {
        OnPieceSteps = 1U,OnWrongStep = 2U,OnSoldierDies = 4U,OnDefenderWins = 8U,OnAttackerWins = 16U,OnPlayerTurnChange = 32U,OnPieceSelected = 64U
    }

    public class GameModel
    {
        public EventHandler? OnAttackerWinsEvent, OnWrongStepEvent, OnSoldierDiesEvent, OnDefenderWinsEvent, OnPieceStepsEvent, OnPieceSelectedEvent, OnPlayerTurnChangeEvent;

        private readonly Dictionary<EventTypeFlag, MethodInfo?> Events = new Dictionary<EventTypeFlag, MethodInfo?>() 
        {
            {EventTypeFlag.OnPieceSteps, typeof(GameModel).GetMethod(nameof(OnPieceSteps),BindingFlags.Instance | BindingFlags.NonPublic)},
            {EventTypeFlag.OnWrongStep, typeof(GameModel).GetMethod(nameof(OnWrongStep),BindingFlags.Instance | BindingFlags.NonPublic)},
            {EventTypeFlag.OnSoldierDies, typeof(GameModel).GetMethod(nameof(OnSoldierDies),BindingFlags.Instance | BindingFlags.NonPublic)},
            {EventTypeFlag.OnDefenderWins, typeof(GameModel).GetMethod(nameof(OnDefenderWins),BindingFlags.Instance | BindingFlags.NonPublic)},
            {EventTypeFlag.OnAttackerWins, typeof(GameModel).GetMethod(nameof(OnAttackerWins),BindingFlags.Instance | BindingFlags.NonPublic)},
            {EventTypeFlag.OnPlayerTurnChange, typeof(GameModel).GetMethod(nameof(OnPlayerTurnChange),BindingFlags.Instance | BindingFlags.NonPublic)},
            {EventTypeFlag.OnPieceSelected, typeof(GameModel).GetMethod(nameof(OnPieceSelected),BindingFlags.Instance | BindingFlags.NonPublic)}
        };
        private Piece? selectedPiece = null;
        private Player currentPlayer;
        private Player[] players = new Player[2];
        private Table table = new Table();
        private GameState gameState = GameState.Playing;

        public Piece? SelectedPiece => selectedPiece;
        public Player CurrentPlayer => currentPlayer;
        public Table Table => table;

        public GameModel(string AttackerName, string DefenderName)
        {
            players[0] = new Player(AttackerName, PlayerSide.Attacker, table,new (int x, int y)[16] { (0,3),(0,4),(0,5),(1,4),(3,0),(4,0),(5,0),(4,1),(3,8),(4,8),(5,8),(4,7),(8,3),(8,4),(8,5),(7,4)},InvokeEvent);
            players[1] = new Player(DefenderName, PlayerSide.Defender, table, new (int x, int y)[9] { (4,4), (4,5), (4,3), (3,4), (5,4), (2,4), (4,2), (6,4), (4,6) }, InvokeEvent);
            currentPlayer = players[1];
        }

        public GameModel(string AttackerName, string DefenderName,(int x,int y)[] AttackerValues, (int x, int y)[] DefenderValues)
        {
            players[0] = new Player(AttackerName, PlayerSide.Attacker, table, AttackerValues,InvokeEvent);
            players[1] = new Player(DefenderName, PlayerSide.Defender, table, DefenderValues, InvokeEvent);
            currentPlayer = players[1];
        }

        public void SelectPieceOrStepWithSelectedPiece(int x, int y)
        {
            if (gameState == GameState.Playing)
            {
                Piece? piece = currentPlayer.AlivePieces.Where(p => p.Place.X == x && p.Place.Y == y).SingleOrDefault();
                if (piece != null)
                {
                    selectedPiece = piece;
                    InvokeEvent(EventTypeFlag.OnPieceSelected, new object[] { });
                }
                else if (selectedPiece != null)
                {
                    selectedPiece.TryStepToPlace(x, y);
                }
            }
        }

        private void InvokeEvent(EventTypeFlag EventTypeFlags, object[] args)
        {
            foreach (EventTypeFlag eventtypeflag in (EventTypeFlag[])Enum.GetValues(typeof(EventTypeFlag)))
            {
                if ((EventTypeFlags & eventtypeflag) == eventtypeflag)
                {
                    Events[eventtypeflag]?.Invoke(this, args);
                }
            }
        }

        private void OnPieceSteps()
        {
            OnPieceStepsEvent?.Invoke(this, new EventArgs());
        }

        private void OnAttackerWins()
        {
            gameState = GameState.GameOver;
            OnAttackerWinsEvent?.Invoke(this, new EventArgs());
        }

        private void OnDefenderWins()
        {
            gameState = GameState.GameOver;
            OnDefenderWinsEvent?.Invoke(this, new EventArgs());
        }

        private void OnWrongStep()
        {
            OnWrongStepEvent?.Invoke(this, new EventArgs());
        }

        private void OnPlayerTurnChange()
        {
            OnPlayerTurnChangeEvent?.Invoke(this, new EventArgs());
        }

        private void OnSoldierDies()
        {
            OnSoldierDiesEvent?.Invoke(this, new EventArgs());
        }

        private void OnPieceSelected()
        {
            OnPieceSelectedEvent?.Invoke(this,new EventArgs());
        }
    }
}