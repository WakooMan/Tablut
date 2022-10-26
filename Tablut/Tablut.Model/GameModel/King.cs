using System;

namespace Tablut.Model.GameModel
{
    public class King : Piece
    {
        public King(Field place, Player player, Action<EventTypeFlag, object[]> InvokeEvent) : base(place, player,InvokeEvent)
        {
        }

        public override void Die()
        {
            base.Die();
            InvokeEvent(EventTypeFlag.OnAttackerWins, new object[] { });
        }

        protected override void OnStepped(int x, int y)
        {
            if (x == 0 || x == 8 || y == 0 || y == 8)
            {
                InvokeEvent(EventTypeFlag.OnDefenderWins, new object[] { });
            }
        }
    }
}