namespace Tablut.Model.GameModel
{
    public class Soldier: Piece
    {
        public Soldier(Field place,Player player, Action<EventTypeFlag, object[]> InvokeEvent) : base(place,player,InvokeEvent)
        {
        }

        private void KillIfNeeded(Field f1, Field f2)
        {
            if (!f1.IsInvalid && !f2.IsInvalid && ((f2.Type == FieldType.Forbidden && f2.Piece == null) || (f2.Piece != null && f2.Piece.Player == Player)) && f1.Piece != null && f1.Piece.Player != Player)
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