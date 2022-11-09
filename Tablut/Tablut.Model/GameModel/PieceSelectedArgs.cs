using System.Collections.Generic;

namespace Tablut.Model.GameModel
{
    public class PieceSelectedArgs
    {
        public readonly IReadOnlyList<Field> AvailableFields;
        public readonly Field SelectedField;

        public PieceSelectedArgs(Field selectedField,IReadOnlyList<Field> availableFields)
        {
            SelectedField = selectedField;
            AvailableFields = availableFields;
        }
    }
}