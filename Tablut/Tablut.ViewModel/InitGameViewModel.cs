using System;
using System.Collections.Generic;
using System.Text;

namespace Tablut.ViewModel
{
    public class InitGameViewModel: ApplicationViewModel
    {
        public string TitleText => "Tablut";
        public string P1Text => "Attacker Player";
        public string P2Text => "Defender Player";
        public string StartText => "Start New Game";
        public string P1Name { get; set; } = "";
        public string P2Name { get; set; } = "";
        public DelegateCommand StartCommand { get; }

        public InitGameViewModel()
        {
            StartCommand = new DelegateCommand(Command_Start);
        }

        private void Command_Start(object obj)
        {
            OnPushState?.Invoke(new GameViewModel(P1Name,P2Name));
        }
    }
}
