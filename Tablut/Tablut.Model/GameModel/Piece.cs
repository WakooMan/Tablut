using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Tablut.Model.GameModel
{
    public abstract class Piece
    {
        protected Field place;
        private readonly Player player;
        private bool isAlive;
        protected readonly Action<EventTypeFlag, object[]> InvokeEvent;
        public bool IsAlive 
        {
            get
            {
                return isAlive;
            }
            protected set 
            {
                if (isAlive != value)
                {
                    isAlive = value;
                }
            }
        }
        public Player Player => player;
        public Field Place => place;

        protected Piece(Field place, Player player, Action<EventTypeFlag, object[]> InvokeEvent)
        {
            this.place = place;
            this.place.Piece = this;
            this.player = player;
            isAlive = true;
            this.InvokeEvent = InvokeEvent;
        }

        public bool TryStepToPlace(int x, int y)
        {
            Field f = place.Table.GetField(x, y);
            if (!f.IsInvalid && f.Type != FieldType.Forbidden && place.Table.AvailableFields(this).Contains(f))
            {
                this.place.Piece = null;
                this.place = f;
                this.place.Piece = this;
                InvokeEvent.Invoke(EventTypeFlag.OnPieceSteps,new object[] { Place.X,Place.Y });
                OnStepped(this.place.X, this.Place.Y);
                return true;
            }
            else
            {
                InvokeEvent.Invoke(EventTypeFlag.OnWrongStep, new object[] { });
                return false;
            }
        }

        protected abstract void OnStepped(int x, int y);

        public virtual void Die()
        {
            IsAlive = false;
            place.Piece = null;
            place = Field.Invalid;
            InvokeEvent(EventTypeFlag.OnPieceDies, new object[] { Player,Place.X, Place.Y });
        }

    }
}