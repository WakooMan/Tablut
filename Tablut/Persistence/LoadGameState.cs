using System;
using System.Collections.Generic;
using System.Text;
using Tablut.ViewModel;

namespace Tablut.Persistence
{
    public class LoadGameState : TablutState
    {
        public LoadGameState() : base(new LoadGameViewModel())
        {
        }

        public LoadGameState(LoadGameViewModel model) : base(model)
        {
        }
    }
}
