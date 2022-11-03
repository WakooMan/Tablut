using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tablut.Model.GameModel;
using Tablut.ViewModel;

namespace Tablut.Persistence
{
    public class GameplayState: TablutState
    {
        private GameModel gameModel;
        public GameplayState() { }

        public GameplayState(GameViewModel model)
        {
            Model = model;
        }

        protected override void OnRead(BinaryReader reader)
        {
            
        }

        protected override void OnWrite(BinaryWriter writer)
        {
            
        }
    }
}
