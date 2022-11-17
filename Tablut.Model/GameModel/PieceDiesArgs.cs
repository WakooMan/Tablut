namespace Tablut.Model.GameModel
{
    public class PieceDiesArgs
    {
        public readonly Player Player;
        public readonly int X, Y;
        public PieceDiesArgs(Player player,int x,int y)
        { 
            Player = player;
            X = x;
            Y = y;
        }
    }
}