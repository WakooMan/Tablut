using System;
using System.Collections.Generic;
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
        OnPieceSteps = 1U,OnWrongStep = 2U,OnPieceDies = 4U,OnDefenderWins = 8U,OnAttackerWins = 16U,OnPieceSelected = 32U,OnBeforePieceSelectionChanged = 64U
    }

    public class GameModel
    {
        public EventHandler OnAttackerWinsEvent, OnWrongStepEvent, OnPieceDiesEvent, OnDefenderWinsEvent, OnPieceStepsEvent, OnPieceSelectedEvent, OnPlayerTurnChangeEvent,OnPausedEvent,OnUnpausedEvent,OnBeforePieceSelectionChangedEvent;

        private readonly Dictionary<EventTypeFlag, MethodInfo> Events = new Dictionary<EventTypeFlag, MethodInfo>()
        {
            {EventTypeFlag.OnPieceSteps, typeof(GameModel).GetMethod(nameof(OnPieceSteps),BindingFlags.Instance | BindingFlags.NonPublic)},
            {EventTypeFlag.OnWrongStep, typeof(GameModel).GetMethod(nameof(OnWrongStep),BindingFlags.Instance | BindingFlags.NonPublic)},
            {EventTypeFlag.OnPieceDies, typeof(GameModel).GetMethod(nameof(OnPieceDies),BindingFlags.Instance | BindingFlags.NonPublic)},
            {EventTypeFlag.OnDefenderWins, typeof(GameModel).GetMethod(nameof(OnDefenderWins),BindingFlags.Instance | BindingFlags.NonPublic)},
            {EventTypeFlag.OnAttackerWins, typeof(GameModel).GetMethod(nameof(OnAttackerWins),BindingFlags.Instance | BindingFlags.NonPublic)},
            {EventTypeFlag.OnPieceSelected, typeof(GameModel).GetMethod(nameof(OnPieceSelected),BindingFlags.Instance | BindingFlags.NonPublic)},
            {EventTypeFlag.OnBeforePieceSelectionChanged, typeof(GameModel).GetMethod(nameof(OnBeforePieceSelectionChanged),BindingFlags.Instance | BindingFlags.NonPublic)}
        };
        private Player currentPlayer;
        private Player[] players = new Player[2];
        private Table table = new Table();
        private GameState gameState = GameState.Playing;

        public GameState GameState => gameState;
        public Player CurrentPlayer => currentPlayer;
        public Table Table => table;
        public IReadOnlyList<Field> AvailableFields => Table.AvailableFields(CurrentPlayer.SelectedPiece);
        public string AttackerName => players[0].Name;
        public string DefenderName => players[1].Name;
        public GameModel(string AttackerName, string DefenderName)
        {
            players[0] = new Player(AttackerName, PlayerSide.Attacker, table,new (int x, int y)[16] { (0,3),(0,4),(0,5),(1,4),(3,0),(4,0),(5,0),(4,1),(3,8),(4,8),(5,8),(4,7),(8,3),(8,4),(8,5),(7,4)},InvokeEvent);
            players[1] = new Player(DefenderName, PlayerSide.Defender, table, new (int x, int y)[9] { (4,4), (4,5), (4,3), (3,4), (5,4), (2,4), (4,2), (6,4), (4,6) }, InvokeEvent);
            currentPlayer = players[1];
        }

        public GameModel(string AttackerName, string DefenderName,(int x,int y)[] AttackerValues, (int x, int y)[] DefenderValues,PlayerSide side)
        {
            players[0] = new Player(AttackerName, PlayerSide.Attacker, table, AttackerValues,InvokeEvent);
            players[1] = new Player(DefenderName, PlayerSide.Defender, table, DefenderValues, InvokeEvent);
            currentPlayer = players[(int)side];
        }

        public void StepOrSelect(int x , int y)
        {
            if (gameState != GameState.Playing)
                return;
            currentPlayer.TryStepOrSelect(x, y);
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

        public void Pause()
        {
            if (gameState == GameState.Playing)
            {
                gameState = GameState.Paused;
                OnPausedEvent?.Invoke(this, new EventArgs());
            }
        }

        public void Unpause()
        {
            if (gameState == GameState.Paused)
            {
                gameState = GameState.Playing;
                OnUnpausedEvent?.Invoke(this, new EventArgs());
            }
        }

        private void OnPieceSteps(int oldx,int oldy,int x, int y)
        {
            OnPieceStepsEvent?.Invoke(new object[] { oldx,oldy,x,y}, new EventArgs());
            currentPlayer = (currentPlayer == players[0]) ? players[1] : players[0];
            OnPlayerTurnChangeEvent?.Invoke(this, new EventArgs());
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

        private void OnPieceDies(Player player,int x, int y)
        {
            if (player.AlivePieces.Count() == 0 && player.Side == PlayerSide.Attacker)
            {
                InvokeEvent(EventTypeFlag.OnDefenderWins,new object[] { });
            }
            OnPieceDiesEvent?.Invoke(new object[] { x,y }, new EventArgs());
        }

        private void OnPieceSelected()
        {
            OnPieceSelectedEvent?.Invoke(this,new EventArgs());
        }

        private void OnBeforePieceSelectionChanged()
        {
            OnBeforePieceSelectionChangedEvent?.Invoke(this, new EventArgs());
        }
    }
}