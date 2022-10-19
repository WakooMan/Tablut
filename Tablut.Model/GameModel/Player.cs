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
        private readonly string name;

        public PlayerSide Side => side;
        public string Name => name;
        public IEnumerable<Piece> Pieces => pieces;
        public IEnumerable<Piece> AlivePieces => pieces.Where(p => p.IsAlive);
        public IEnumerable<Piece> DeadPieces => pieces.Where(p => !p.IsAlive);

        public Player(string name,PlayerSide side,Table table,(int x,int y)[] values, EventHandler OnPieceSteps, EventHandler OnWrongStep, EventHandler OnKingDies, EventHandler OnDefenderWins,EventHandler OnSoldierDies)
        {
            this.name = name;
            this.side = side;
            int pieceCount = (this.side == PlayerSide.Attacker) ? 16 : 9;
            pieces = new Piece[pieceCount];
            for (int i = 0; i < values.Length; i++)
            {
                if (this.side == PlayerSide.Defender && i == 0)
                {
                    pieces[i] = new King(table.GetField(values[i].x, values[i].y), this, OnPieceSteps, OnWrongStep, OnKingDies, OnDefenderWins);
                }
                else
                {
                    pieces[i] = new Soldier(table.GetField(values[i].x, values[i].y), this, OnPieceSteps, OnWrongStep,OnSoldierDies);
                }
            }
        }
    }
}