namespace Tablut.Model.GameModel
{
    public enum FieldType
    {
        Normal = 0, Forbidden = 1, Invalid = 2
    }
    public class Field
    {
        private int x;
        private int y;
        private Piece? piece;
        private Table? table;
        private FieldType type;

        public bool IsInvalid => type == FieldType.Invalid;

        public static Field Invalid 
        { 
            get 
            {
                return new Field(null, FieldType.Invalid, -1, -1);
            } 
        }
        public FieldType Type => type;
        public int X => x;
        public int Y => y;
        public Table? Table => table;
        public Piece? Piece
        {
            get 
            {
                return piece;
            }
            set 
            {
                if (piece is null)
                {
                    piece = value;
                }
            }
        }

        public Field(Table? table,FieldType type,int x, int y)
        {
            this.table = table;
            this.type = type;
            this.x = x;
            this.y = y;
            piece = null;
        }
    }
}