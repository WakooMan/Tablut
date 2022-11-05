using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tablut.Model.GameModel;
using Tablut.Persistence;
using Xamarin.Forms;

namespace Tablut.ViewModel
{
    public class GameViewModel: ApplicationViewModel
    {
        private readonly GameModel _model;
        public ObservableCollection<FieldViewModel> Fields { get; private set; } = new ObservableCollection<FieldViewModel>();

        public GameModel Model => _model;
        public string SaveFileName { get; }

        public string TitleText => "Tablut";
        public string MenuText => "Game Menu";
        public string ContinueText => "Continue Game";
        public string SaveText => "Save Game";
        public string SaveAndExitText => "Save And Exit";
        public string ExitText => "Exit Game";
        public string AttackerName => _model.Attacker.Name;
        public string DefenderName => _model.Defender.Name;
        public DelegateCommand MenuCommand { get; private set; }
        public DelegateCommand ContinueCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand SaveAndExitCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public GameViewModel(GameModel model,string saveFileName)
        {
            _model = model;
            SaveFileName = saveFileName;
            Initialize();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Fields.Add(CreateFieldViewModel(_model.Table.GetField(i, j)));
                }
            }
        }

        private void Initialize()
        {
            MenuCommand = new DelegateCommand(Command_Menu);
            ContinueCommand = new DelegateCommand(Command_Continue);
            SaveCommand = new DelegateCommand(Command_Save);
            SaveAndExitCommand = new DelegateCommand(Command_SaveAndExit);
            ExitCommand = new DelegateCommand(Command_Exit);
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
            _model.OnBeforePieceSelectionChangedEvent += (o, e) =>
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
                OnPushState(new GameMenuViewModel(new DelegateCommand(Command_Continue), new DelegateCommand(Command_Save), new DelegateCommand(Command_SaveAndExit), new DelegateCommand(Command_Exit)));
            };
            _model.OnUnpausedEvent += (o, e) =>
            {
                OnPushState(this);
            };
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

        private void Command_Menu(object param)
        {
            _model.Pause();
        }

        private void Command_Continue(object param)
        {
            _model.Unpause();
        }

        private void Command_Save(object param)
        {
            DependencyService.Get<ITablutPersistence>().SaveGameState(SaveFileName + ".tablut",new SaveGameState(this));
        }

        private void Command_SaveAndExit(object param)
        {
            Command_Save(param);
            OnPushState?.Invoke(new MainMenuViewModel());
        }

        private void Command_Exit(object param)
        {
            OnPushState?.Invoke(new MainMenuViewModel());
        }
    }
}
