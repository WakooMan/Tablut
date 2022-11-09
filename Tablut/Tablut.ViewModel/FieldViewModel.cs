using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tablut.Model.GameModel;

namespace Tablut.ViewModel
{
    public enum PieceType 
    {
        None, SelectedDefenderKing, SelectedDefenderSoldier, SelectedAttackerSoldier, DefenderSoldier, DefenderKing, AttackerSoldier
    }

    public enum FieldType
    {
        Normal, Available
    }
    public class FieldViewModel : BindingSource
    {
        public readonly int X;
        public readonly int Y;
        private FieldType _field;
        private PieceType _piece;
        
        private DelegateCommand _stepOrSelectCommand;
        public DelegateCommand StepOrSelectCommand 
        {
            get
            {
                return _stepOrSelectCommand;
            }
            private set
            {
                if (_stepOrSelectCommand != value)
                {
                    _stepOrSelectCommand = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public FieldType Field
        {
            get
            {
                return _field;
            }
            set
            {
                if (_field != value)
                {
                    _field = value;
                    OnPropertyChanged();
                }
            }
        }
        public PieceType Piece 
        {
            get
            {
                return _piece;
            }
            set
            {
                if (_piece != value)
                {
                    _piece = value;
                    OnPropertyChanged();
                }
            }
        }

        public FieldViewModel(int x,int y,PieceType piece,FieldType field,DelegateCommand stepOrSelectCommand)
        {
            X = x;
            Y = y;
            _piece = piece;
            _field = field;
            StepOrSelectCommand = stepOrSelectCommand;
        }
    }
}
