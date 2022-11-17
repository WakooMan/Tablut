using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tablut.ViewModel;

namespace Tablut.Persistence
{
    public class InitGameState: TablutState
    {
        public InitGameState(): base(new InitGameViewModel()) { }
        public InitGameState(InitGameViewModel model) : base(model) { }

        protected override void OnRead(BinaryReader reader)
        {
            InitGameViewModel model = (InitGameViewModel)Model;
            model.FileName = reader.ReadString();
            model.P1Name = reader.ReadString();
            model.P2Name = reader.ReadString();
        }

        protected override void OnWrite(BinaryWriter writer)
        {
            InitGameViewModel model = (InitGameViewModel)Model;
            writer.Write(model.FileName);
            writer.Write(model.P1Name);
            writer.Write(model.P2Name);
        }
    }
}
