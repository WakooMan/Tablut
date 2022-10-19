using System.Drawing;

namespace Tablut.Model.GameModel
{
    public abstract class Piece
    {
        protected Field place;
        private Player player;
        private bool isAlive;
        private readonly EventHandler OnPieceSteps;
        private readonly EventHandler OnWrongStep;

        public Color Color => (player.Side == PlayerSide.Attacker)?Color.Red:Color.Blue;
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
        public Field Place => place;

        protected Piece(Field place, Player player, EventHandler OnPieceSteps, EventHandler OnWrongStep)
        {
            this.place = place;
            this.place.Piece = this;
            this.player = player;
            isAlive = true;
            this.OnPieceSteps = OnPieceSteps;
            this.OnWrongStep = OnWrongStep;
        }

        public void TryStepToPlace(int x, int y)
        {
            Field place = this.place.Table.GetField(x, y);
            if (!place.IsInvalid && place.Type != FieldType.Forbidden && StepValidation(place.X, place.Y))
            {
                this.place = place;
                this.place.Piece = this;
                OnPieceSteps?.Invoke(this,new EventArgs());
                OnStepped(this.place.X, this.Place.Y);
            }
            else
            {
                OnWrongStep?.Invoke(this, new EventArgs());
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

        public abstract void Die();

    }
}