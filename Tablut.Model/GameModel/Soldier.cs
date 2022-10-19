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
            //Implement fully (handle dieing)
            OnSoldierDies.Invoke(this, new EventArgs());
        }

        protected override void OnStepped(int x, int y)
        {
            //Implement fully (handle hitting enemy soldiers and else)
        }
    }
}