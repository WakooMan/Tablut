namespace Tablut.Model.GameModel
{
    public class Table
    {
        private Field[,] fields;

        public Field GetField(int x, int y)
        {
            return (x <= 8 && x >= 0 && y <= 8 && y >= 0)?fields[x,y]:Field.Invalid;
        }
        public Table()
        {
            fields = new Field[9,9];
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    fields[x,y] = new Field(this,(x == 4 && y == 4)?FieldType.Forbidden:FieldType.Normal,x,y);
                }
            }
        }
    }
}