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
        private readonly FieldViewModel[][] _fields = new FieldViewModel[9][];
        private readonly GameMenuViewModel Menu;
        public FieldViewModel[][] Fields => _fields;

        public GameModel Model => _model;
        public string SaveFileName { get; }
        public string TitleText => "Tablut";
        public string MenuText => "Game Menu";
        public string AttackerName => _model.Attacker.Name;
        public string AttackerText => "Attacker";
        public string DefenderText => "Defender";
        public string AttackerAttributes => (_model.Attacker == _model.CurrentPlayer) ?"Bold":"";
        public string DefenderAttributes => (_model.Defender == _model.CurrentPlayer) ? "Bold" : "";
        public string DefenderName => _model.Defender.Name;
        public DelegateCommand MenuCommand { get; private set; }

        public GameViewModel(GameModel model,string saveFileName)
        {
            _model = model;
            SaveFileName = saveFileName;
            Menu = new GameMenuViewModel(this, new DelegateCommand(Command_Continue), new DelegateCommand(Command_Save), new DelegateCommand(Command_SaveAndExit), new DelegateCommand(Command_Exit));
            Initialize();
            for (int i = 0; i < 9; i++)
            {
                Fields[i] = new FieldViewModel[9];
                for (int j = 0; j < 9; j++)
                {
                    Fields[i][j] = CreateFieldViewModel(_model.Table.GetField(i, j));
                }
            }
        }

        private void Initialize()
        {
            MenuCommand = new DelegateCommand(Command_Menu);
            _model.OnPieceDiesEvent += (o, e) =>
            {
                FieldViewModel fVM = GetFieldViewModel(e.X, e.Y);
                fVM.Piece = PieceType.None;
            };
            _model.OnPieceStepsEvent += (o, e) =>
            {
                Field From = _model.Table.GetField(e.OldX, e.OldY);
                Field To = _model.Table.GetField(e.NewX, e.NewY);
                FieldViewModel FromVM = GetFieldViewModel(e.OldX, e.OldY);
                FieldViewModel ToVM = GetFieldViewModel(e.NewX, e.NewY);
                FromVM.Piece = ConvertToPieceType(From.Piece);
                ToVM.Piece = ConvertToPieceType(To.Piece);
            };
            _model.OnPieceSelectionChangedEvent += (o, e) =>
            {
                FieldViewModel OldSelectedfvm = GetFieldViewModel(e.SelectedField.X, e.SelectedField.Y);
                OldSelectedfvm.Piece = ConvertToPieceType(e.SelectedField.Piece);
                foreach (Field field in e.AvailableFields)
                {
                    FieldViewModel availablefvm = GetFieldViewModel(field.X, field.Y);
                    availablefvm.Field = FieldType.Normal;
                }
            };
            _model.OnPieceSelectedEvent += (o, e) =>
            {
                FieldViewModel fvm = GetFieldViewModel(e.SelectedField.X, e.SelectedField.Y);
                fvm.Piece = ConvertToPieceType(e.SelectedField.Piece);
                foreach (Field field in e.AvailableFields)
                {
                    FieldViewModel availablefvm = GetFieldViewModel(field.X, field.Y);
                    availablefvm.Field = FieldType.Available;
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
                OnPushState(Menu);
            };
            _model.OnUnpausedEvent += (o, e) =>
            {
                OnPushState(this);
            };
        }
        private FieldViewModel GetFieldViewModel(int X, int Y)
        {
            return Fields[X][Y];
        }

        private FieldViewModel CreateFieldViewModel(Field f)
        {
            return new FieldViewModel(f.X, f.Y,ConvertToPieceType(f.Piece),(_model.AvailableFields.Contains(f))?FieldType.Available:FieldType.Normal,new DelegateCommand(Command_StepOrSelect));
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

        private PieceType ConvertToPieceType(Piece piece)
        {
            if (piece == null)
            {
                return PieceType.None;
            }
            else
            {
                if (piece.Player.Side == PlayerSide.Attacker)
                {
                    if (piece == _model.CurrentPlayer.SelectedPiece)
                    {
                        return PieceType.SelectedAttackerSoldier;
                    }
                    else
                    {
                        return PieceType.AttackerSoldier;
                    }
                }
                else if (piece.Player.Side == PlayerSide.Defender)
                {
                    if (piece == _model.CurrentPlayer.SelectedPiece)
                    {
                        if (piece is King)
                        {
                            return PieceType.SelectedDefenderKing;
                        }
                        else if (piece is Soldier)
                        {
                            return PieceType.SelectedDefenderSoldier;
                        }
                    }
                    else
                    {
                        if (piece is King)
                        {
                            return PieceType.DefenderKing;
                        }
                        else if (piece is Soldier)
                        {
                            return PieceType.DefenderSoldier;
                        }
                    }
                }
                return PieceType.None;
            }
        }
    }
}
