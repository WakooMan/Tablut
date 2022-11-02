using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tablut.Model.GameModel;

namespace Tablut.ViewModel
{
    public class GameViewModel: ApplicationViewModel
    {
        private GameModel _model;
        public ObservableCollection<FieldViewModel> Fields { get; private set; } = new ObservableCollection<FieldViewModel>();

        public GameViewModel(string p1, string p2)
        {
            _model = new GameModel(p1,p2);
            _model.OnPieceDiesEvent += (o, e) =>
            {
                object[] args = (object[])o;
                int x = (int)args[0], y = (int)args[1];
                FieldViewModel fVM = GetFieldViewModel(x, y);
                fVM.Piece = null;
            };
            _model.OnPieceStepsEvent += (o, e) =>
            {
                object[] args = (object[])o;
                int oldx = (int)args[0], oldy = (int)args[1];
                int newx = (int)args[2], newy = (int)args[3];
                Field From = _model.Table.GetField(oldx, oldy);
                Field To = _model.Table.GetField(newx, newy);
                FieldViewModel FromVM = GetFieldViewModel(oldx, oldy);
                FieldViewModel ToVM = GetFieldViewModel(newx, newy);
                FromVM.Piece = From.Piece;
                ToVM.Piece = To.Piece;
                ToVM.IsSelected = false;
            };
            _model.OnBeforePieceSelectionChangedEvent += (o,e) =>
            {
                Field f = _model.CurrentPlayer.SelectedPiece.Place;
                FieldViewModel fvm = GetFieldViewModel(f.X, f.Y);
                fvm.IsSelected = false;
                foreach (Field field in _model.AvailableFields)
                {
                    FieldViewModel availablefvm = GetFieldViewModel(field.X, field.Y);
                    availablefvm.Available = false;
                }

            };
            _model.OnPieceSelectedEvent += (o, e) =>
            {
                Field f = _model.CurrentPlayer.SelectedPiece.Place;
                FieldViewModel fvm = GetFieldViewModel(f.X, f.Y);
                fvm.IsSelected = true;
                foreach (Field field in _model.AvailableFields)
                {
                    FieldViewModel availablefvm = GetFieldViewModel(field.X, field.Y);
                    availablefvm.Available = true;
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
            return new FieldViewModel(f.X, f.Y,f.Piece,_model.CurrentPlayer.SelectedPiece == f.Piece,_model.AvailableFields.Contains(f),new DelegateCommand(Command_StepOrSelect));
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
