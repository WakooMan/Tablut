namespace Tablut.Model.GameModel
{
    public class King : Piece
    {
        private readonly EventHandler OnKingDies;
        private readonly EventHandler OnDefenderWins;
        public King(Field place, Player player, EventHandler OnPieceSteps, EventHandler OnWrongStep,EventHandler OnKingDies,EventHandler OnDefenderWins) : base(place, player, OnPieceSteps, OnWrongStep)
        {
            this.OnKingDies = OnKingDies;
            this.OnDefenderWins = OnDefenderWins;
        }

        public override void Die()
        {
            IsAlive = false;
            place.Piece = null;
            place = Field.Invalid;
            OnKingDies?.Invoke(this,new EventArgs());
        }

        protected override void OnStepped(int x, int y)
        {
            if (x == 0 || x == 8 || y == 0 || y == 8)
            {
                OnDefenderWins?.Invoke(this,new EventArgs());
            }
        }
    }
}