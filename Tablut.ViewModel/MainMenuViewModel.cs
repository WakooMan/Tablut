using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Tablut.ViewModel
{
    public class MainMenuViewModel: ApplicationViewModel
    {
        public string TitleText => "Tablut";
        public string NewGameText => "New Game";
        public string LoadGameText => "Load Game";
        public string ExitText => "Exit";

        public DelegateCommand NewGameCommand { get; }
        public DelegateCommand LoadGameCommand { get; }
        public DelegateCommand ExitCommand { get; }

        public MainMenuViewModel()
        {
            NewGameCommand = new DelegateCommand(Command_NewGame);
            LoadGameCommand = new DelegateCommand(Command_LoadGame);
            ExitCommand = new DelegateCommand(Command_Exit);
        }

        private void Command_NewGame(object obj)
        {
            OnPushState?.Invoke(new InitGameViewModel());
        }

        private void Command_LoadGame(object obj)
        {
            OnPushState?.Invoke(new LoadGameViewModel());
        }

        private void Command_Exit(object obj)
        {
            string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "save.dat");
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            ExitGame?.Invoke();
        }

    }
}
