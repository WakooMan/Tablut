using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tablut.Model.GameModel;

namespace Tablut.ViewModel
{
    public class GameViewModel: BindingSource
    {
        private GameModel _model;
        public ObservableCollection<FieldViewModel> Fields { get; private set; } = new ObservableCollection<FieldViewModel>();

        public GameViewModel(GameModel model)
        {
            _model = model;
            _model.OnPieceStepsEvent += (o, e) =>
            {

            };
            _model.OnPieceUnselectedEvent += (o,e)
            {
                
            };
            _model.OnPieceSelectedEvent += (o, e) =>
            {
                Field f = _model.CurrentPlayer.SelectedPiece.Place;
                FieldViewModel fvm = GetFieldViewModel(f.X, f.Y);
                fvm.ImageSource = "Selected" + fvm.ImageSource;
                foreach (Field field in _model.AvailableFields)
                {
                    FieldViewModel availablefvm = GetFieldViewModel(f.X, f.Y);
                    availablefvm.ImageSource = "Available" + availablefvm.ImageSource;
                }
            };
            _model.OnDefenderWinsEvent += (o, e) =>
            {
            };
            _model.OnAttackerWinsEvent += (o, e) =>
            {
            };
            _model.OnWrongStepEvent += (o, e) =>
            {
            };
            _model.OnPlayerTurnChangeEvent += (o, e) =>
            {
            };
            _model.OnPausedEvent += (o, e) =>
            {
            };
            _model.OnUnpausedEvent += (o, e) =>
            {
            };
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Fields.Add(CreateFieldViewModel(_model.Table.GetField(i, j)));
                }
            }
        }

        private FieldViewModel GetFieldViewModel(int X, int Y)
        {
            return Fields[X * 9 + Y];
        }

        private FieldViewModel CreateFieldViewModel(Field f)
        {
            string imagesrc = "Field.png";
            if (f.Piece != null)
            {
                if (f.Piece.Player.Side == PlayerSide.Attacker)
                {
                    imagesrc = "AttackerSoldier" + imagesrc;
                }
                else
                {
                    if (f.Piece is King)
                    {
                        imagesrc = "DefenderKing" + imagesrc;
                    }
                    else
                    {
                        imagesrc = "DefenderSoldier" + imagesrc;
                    }
                }
            }
            imagesrc = "Resources/" + imagesrc;
            return new FieldViewModel(f.X, f.Y, imagesrc, new DelegateCommand(Command_StepOrSelect));
        }

        private void Command_StepOrSelect(object param)
        {
            if (param != null && param is FieldViewModel field)
            {
                _model.StepOrSelect(field.X,field.Y);
            }
        }
    }
}
