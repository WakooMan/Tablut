using System;
using System.Collections.Generic;
using System.Text;
using Tablut.Model.GameModel;
using Tablut.ViewModel;

namespace Tablut.ViewModel
{
    public class GameOverViewModel: ApplicationViewModel
    {
        public GameViewModel Game { get; }
        public PlayerSide Side { get; }
        public string MainMenuText => "Main Menu";
        public string GameWinnerName { get; }
        public DelegateCommand MainMenuCommand { get; }
        public GameOverViewModel(string gameWinnerName,GameViewModel game,PlayerSide side)
        {
            GameWinnerName = "Winner: " + gameWinnerName;
            Game = game;
            Side = side;
            MainMenuCommand = new DelegateCommand(Command_MainMenu);
        }

        private void Command_MainMenu(object param)
        {
            OnPopToRootState?.Invoke();
        }

    }
}
