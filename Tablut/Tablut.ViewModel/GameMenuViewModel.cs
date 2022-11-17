using System.Collections.ObjectModel;
using Tablut.Model.GameModel;
using Tablut.Persistence;
using Xamarin.Forms;

namespace Tablut.ViewModel
{
    public class GameMenuViewModel : ApplicationViewModel
    {
        private bool _isInGameMenu = false;
        public bool IsInGameMenu 
        {
            get 
            {
                return _isInGameMenu;
            }
            set 
            {
                if (_isInGameMenu != value)
                {
                    _isInGameMenu = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ContinueText => "Continue Game";
        public string SaveText => "Save Game";
        public string SaveAndExitText => "Save And Exit";
        public string ExitText => "Exit Game";
        public DelegateCommand ContinueCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand SaveAndExitCommand { get; }
        public DelegateCommand ExitCommand { get; }
        public GameViewModel Game { get; }

        public GameMenuViewModel(GameViewModel game)
        {
            ContinueCommand = new DelegateCommand(Command_Continue);
            SaveCommand = new DelegateCommand(Command_Save);
            SaveAndExitCommand = new DelegateCommand(Command_SaveAndExit);
            ExitCommand = new DelegateCommand(Command_Exit);
            Game = game;
        }

        private void Command_Continue(object param)
        {
            Game.Model.Unpause();
        }

        private async void Command_Save(object param)
        {
            await DependencyService.Get<ITablutPersistence>().SaveGameState(Game.SaveFileName + ".tablut", new SaveGameState(Game));
            Command_Continue(param);
        }

        private async void Command_SaveAndExit(object param)
        {
            await DependencyService.Get<ITablutPersistence>().SaveGameState(Game.SaveFileName + ".tablut", new SaveGameState(Game));
            Command_Exit(param);
        }

        private void Command_Exit(object param)
        {
            OnPushState?.Invoke(new MainMenuViewModel());
        }
    }
}