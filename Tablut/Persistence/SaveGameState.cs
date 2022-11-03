using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tablut.Model.GameModel;
using Tablut.ViewModel;

namespace Tablut.Persistence
{
    public class SaveGameState : TablutState
    {
        private GameModel gameModel;
        public SaveGameState() { }

        public SaveGameState(GameViewModel model)
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
