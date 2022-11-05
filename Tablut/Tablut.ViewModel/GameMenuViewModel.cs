using System.Collections.ObjectModel;
using Tablut.Model.GameModel;
using Tablut.Persistence;
using Xamarin.Forms;

namespace Tablut.ViewModel
{
    public class GameMenuViewModel : ApplicationViewModel
    {
        public string ContinueText => "Continue Game";
        public string SaveText => "Save Game";
        public string SaveAndExitText => "Save And Exit";
        public string ExitText => "Exit Game";
        public DelegateCommand ContinueCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand SaveAndExitCommand { get; }
        public DelegateCommand ExitCommand { get; }

        public GameMenuViewModel(DelegateCommand continueCommand, DelegateCommand saveCommand, DelegateCommand saveAndExitCommand, DelegateCommand exitCommand)
        {
            ContinueCommand = continueCommand;
            SaveCommand = saveCommand;
            SaveAndExitCommand = saveAndExitCommand;
            ExitCommand = exitCommand;
        }
    }
}