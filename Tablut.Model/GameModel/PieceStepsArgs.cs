namespace Tablut.Model.GameModel
{
    public class PieceStepsArgs
    {
        public readonly int OldX,OldY,NewX,NewY;
        public PieceStepsArgs(int oldX,int oldY,int newX,int newY)
        { 
            OldX = oldX;
            OldY = oldY;
            NewX = newX;
            NewY = newY;
        }
    }
}