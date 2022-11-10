using System;
using System.Collections.Generic;
using System.Text;
using Tablut.Model.GameModel;

namespace Tablut.ViewModel
{
    public class InitGameViewModel: ApplicationViewModel
    {
        public string TitleText => "Tablut";
        public string P1Text => "Attacker Player";
        public string P2Text => "Defender Player";
        public string StartText => "Start New Game";
        public string BackText => "Go Back To Menu";
        public string FileNameText => "Save Filename";
        public string FileName { get; set; } = "";
        public string P1Name { get; set; } = "";
        public string P2Name { get; set; } = "";
        public DelegateCommand StartCommand { get; }
        public DelegateCommand BackCommand { get; }

        public InitGameViewModel()
        {
            StartCommand = new DelegateCommand(Command_Start);
            BackCommand = new DelegateCommand(Command_Back);
        }

        private void Command_Start(object obj)
        {
            GameViewModel model = new GameViewModel(new GameModel(P1Name, P2Name), FileName);
            OnPushState?.Invoke(model);
        }

        private void Command_Back(object obj)
        {
            OnPushState?.Invoke(new MainMenuViewModel());
        }
    }
}
