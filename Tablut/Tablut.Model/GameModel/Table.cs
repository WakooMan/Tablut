using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Tablut.Model.GameModel
{
    public class Table
    {
        private Field[,] fields;

        public Field GetField(int x, int y)
        {
            return (x <= 8 && x >= 0 && y <= 8 && y >= 0)?fields[x,y]:Field.Invalid;
        }

        public IReadOnlyList<Field> AvailableFields(Piece piece)
        {
            List<Field> result = new List<Field>();
            if (piece != null && !piece.Place.IsInvalid)
            {
                int x = piece.Place.X;
                bool xdone = false;
                bool xincrement = true;
                while (!xdone)
                {
                    if (x >= 0 && x <= 8 && x != piece.Place.X && fields[x,piece.Place.Y].Piece == null && fields[x, piece.Place.Y].Type != FieldType.Forbidden)
                    {
                        result.Add(fields[x, piece.Place.Y]);
                    }
                    else if (x != piece.Place.X && (x < 0 || x > 8 || fields[x, piece.Place.Y].Piece != null || fields[x, piece.Place.Y].Type == FieldType.Forbidden))
                    {
                        if (!xincrement)
                        {
                            xdone = true;
                        }
                        xincrement = !xincrement;
                        x = piece.Place.X;
                    }
                    x += (xincrement) ? 1 : -1;
                }

                int y = piece.Place.Y;
                bool ydone = false;
                bool yincrement = true;
                while (!ydone)
                {
                    if (y >= 0 && y <= 8 && y != piece.Place.Y && fields[piece.Place.X,y].Piece == null && fields[piece.Place.X, y].Type != FieldType.Forbidden)
                    {
                        result.Add(fields[piece.Place.X, y]);
                    }
                    else if (y != piece.Place.Y && (y < 0 || y > 8 || fields[piece.Place.X, y].Piece != null || fields[piece.Place.X, y].Type == FieldType.Forbidden))
                    {
                        if (!yincrement)
                        {
                            ydone = true;
                        }
                        yincrement = !yincrement;
                        y = piece.Place.Y;
                    }
                    y += (yincrement) ? 1 : -1;
                }
            }
            return result;
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