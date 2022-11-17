using System;
using System.Collections.Generic;
using System.Text;

namespace Tablut.ViewModel
{
    public class SavingGameViewModel: ApplicationViewModel
    {
        public GameViewModel Game { get; set; }
        public SavingGameViewModel(GameViewModel game)
        {
            Game = game;
        }
    }
}
