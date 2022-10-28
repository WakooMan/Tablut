using System;
using System.Collections.Generic;
using System.Linq;

namespace Tablut.Model.GameModel
{
    public enum PlayerSide
    {
        Attacker = 0, Defender = 1
    }
    public class Player
    {
        private readonly PlayerSide side;
        private readonly Piece[] pieces;
        private readonly string name;
        private readonly Action<EventTypeFlag, object[]> InvokeEvent;
        public Piece SelectedPiece { get; private set; } = null;

        public PlayerSide Side => side;
        public string Name => name;
        public IEnumerable<Piece> Pieces => pieces;
        public IEnumerable<Piece> AlivePieces => pieces.Where(p => p.IsAlive);
        public IEnumerable<Piece> DeadPieces => pieces.Where(p => !p.IsAlive);
        public Player(string name,PlayerSide side,Table table,(int x,int y)[] values, Action<EventTypeFlag, object[]> InvokeEvent)
        {
            this.InvokeEvent = InvokeEvent;
            this.name = name;
            this.side = side;
            pieces = new Piece[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                if (this.side == PlayerSide.Defender && i == 0)
                {
                    pieces[i] = new King(table.GetField(values[i].x, values[i].y), this,InvokeEvent);
                }
                else
                {
                    pieces[i] = new Soldier(table.GetField(values[i].x, values[i].y), this,InvokeEvent);
                }
            }
        }

        public void TryStepOrSelect(int x, int y)
        {
            Piece piece = AlivePieces.Where(p => p.Place.X == x && p.Place.Y == y).SingleOrDefault();
            if (piece != null)
            {
                SelectedPiece = piece;
                InvokeEvent(EventTypeFlag.OnPieceSelected, new object[] { });
            }
            else
            {
                if (SelectedPiece != null)
                {
                    if (SelectedPiece.TryStepToPlace(x, y))
                    {
                        SelectedPiece = null;
                    }
                }
                else
                {
                    InvokeEvent(EventTypeFlag.OnWrongStep, new object[] { });
                }
            }
        }
    }
}