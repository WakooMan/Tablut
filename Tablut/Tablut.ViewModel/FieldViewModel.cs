using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tablut.Model.GameModel;

namespace Tablut.ViewModel
{
    public class FieldViewModel : BindingSource
    {
        public readonly int X;
        public readonly int Y;
        private bool _isSelected, _available;
        private Piece _piece;
        private string _imagesource;
        public string ImageSource
        {
            get
            {
                return _imagesource;
            }
            private set 
            {
                if (_imagesource != value)
                {
                    _imagesource = value;
                    OnPropertyChanged();
                }
            }
        }

        private void SetImageSource()
        {
            string imagesrc = "Field.png";
            if (Piece != null)
            {
                imagesrc = ((Piece is King) ? "King" : "Soldier") + imagesrc;
                imagesrc = ((Piece.Player.Side == PlayerSide.Attacker) ? "Attacker" : "Defender") + imagesrc;
                imagesrc = ((IsSelected) ? "Selected" : "") + imagesrc;
            }
            else
            {
                imagesrc = ((Available) ? "Available" : "") + imagesrc;
            }
            imagesrc = "Resources/" + imagesrc;
            ImageSource = imagesrc;
        }
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
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    SetImageSource();
                }
            } 
        }
        public bool Available
        {
            get
            {
                return _available;
            }
            set
            {
                if (_available != value)
                {
                    _available = value;
                    SetImageSource();
                }
            }
        }
        public Piece Piece 
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
                    SetImageSource();
                }
            }
        }

        public FieldViewModel(int x,int y,Piece piece,bool isSelected,bool available,DelegateCommand stepOrSelectCommand)
        {
            X = x;
            Y = y;
            _piece = piece;
            _isSelected = isSelected;
            _available = available;
            StepOrSelectCommand = stepOrSelectCommand;
            SetImageSource();
        }
    }
}
