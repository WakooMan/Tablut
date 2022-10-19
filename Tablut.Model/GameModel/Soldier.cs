namespace Tablut.Model.GameModel
{
    public class Soldier: Piece
    {
        private readonly EventHandler OnSoldierDies;
        public Soldier(Field place,Player player,EventHandler OnPieceSteps,EventHandler OnWrongStep,EventHandler OnSoldierDies): base(place,player,OnPieceSteps,OnWrongStep)
        {
            this.OnSoldierDies = OnSoldierDies;
        }

        public override void Die()
        {
            IsAlive = false;
            place.Piece = null;
            place = Field.Invalid;
            OnSoldierDies?.Invoke(this, new EventArgs());
        }

        private void KillIfNeeded(Field f1, Field f2)
        {
            if (!f1.IsInvalid && !f2.IsInvalid && (f2.Type == FieldType.Forbidden || (f2.Piece != null && f2.Piece.Color == Color)) && f1.Piece != null && f1.Piece.Color != Color)
            {
                f1.Piece.Die();
            }
        }

        protected override void OnStepped(int x, int y)
        {
            KillIfNeeded(Place.Table.GetField(x, y - 1), Place.Table.GetField(x, y - 2));
            KillIfNeeded(Place.Table.GetField(x + 1, y), Place.Table.GetField(x + 2, y));
            KillIfNeeded(Place.Table.GetField(x - 1, y), Place.Table.GetField(x - 2, y));
            KillIfNeeded(Place.Table.GetField(x, y + 1), Place.Table.GetField(x, y + 2));
        }
    }
}