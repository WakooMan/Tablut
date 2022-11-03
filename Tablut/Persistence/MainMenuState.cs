using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tablut.ViewModel;

namespace Tablut.Persistence
{
    public class MainMenuState : TablutState
    {
        public MainMenuState() { Model = new MainMenuViewModel(); }
    }
}
