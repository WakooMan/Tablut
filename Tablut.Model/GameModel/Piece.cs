using System.Drawing;

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
            Field place = this.place.Table.GetField(x, y);
            if (!place.IsInvalid && place.Type != FieldType.Forbidden && StepValidation(place.X, place.Y))
            {
                this.place = place;
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

        private bool StepValidation(int x, int y)
        {
            Table? table = this.place.Table;
            if (this.place.X == x && y != this.place.Y)
            {
                for (int iy = 0; iy < 9; iy++)
                {
                    if (table.GetField(x,iy).Piece != null && ((iy <= y && iy > this.place.Y) || (iy >= y && iy < this.place.Y)))
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (this.place.Y == y && x != this.place.X)
            {

                for (int ix = 0; ix < 9; ix++)
                {
                    if (table.GetField(ix, y).Piece != null && ((ix <= x && ix > this.place.X) || (ix >= x && ix < this.place.X)))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
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