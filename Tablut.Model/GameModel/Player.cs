namespace Tablut.Model.GameModel
{
    public enum PlayerSide
    {
        Attacker = 0, Defender = 1
    }
    public class Player
    {
        private readonly PlayerSide side;
        private readonly Piece[] pieces;

        public PlayerSide Side => side;
        public IEnumerable<Piece> Pieces => pieces;
        public IEnumerable<Piece> AlivePieces => pieces.Where(p => p.IsAlive);
        public IEnumerable<Piece> DeadPieces => pieces.Where(p => !p.IsAlive);

        public Player(PlayerSide side,Table table)
        {
            this.side = side;
            int pieceCount = (this.side == PlayerSide.Attacker) ? 12 : 9;
            pieces = new Piece[pieceCount];
            if (this.side == PlayerSide.Attacker)
            {
                //add all the soldiers in the right place
            }
            else
            {
                //add all the soldiers and the king in the right place
            }
        }

        public void StepToPlaceWithPiece(int px,int py,int x, int y)
        {
            Piece piece = AlivePieces.Where(p=> p.Place.X == px && p.Place.Y == py).Single();
            piece.TryStepToPlace(x, y);
        }
    }
}