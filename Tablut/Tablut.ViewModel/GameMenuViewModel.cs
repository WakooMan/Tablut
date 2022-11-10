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

        public GameMenuViewModel(GameViewModel game,DelegateCommand continueCommand, DelegateCommand saveCommand, DelegateCommand saveAndExitCommand, DelegateCommand exitCommand)
        {
            ContinueCommand = continueCommand;
            SaveCommand = saveCommand;
            SaveAndExitCommand = saveAndExitCommand;
            ExitCommand = exitCommand;
            Game = game;
        }
    }
}