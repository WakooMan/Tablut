using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tablut.ViewModel;

namespace Tablut.Persistence
{
    public abstract class TablutState
    {
        public ApplicationViewModel Model { get; protected set; }
        protected TablutState(ApplicationViewModel model)
        {
            Model = model;
        }

        public static TablutState Read(BinaryReader reader)
        {
            string typeName = reader.ReadString();
            TablutState state = (TablutState)Activator.CreateInstance(Type.GetType(typeName));
            state.OnRead(reader);
            return state;
        }
        public static void Write(BinaryWriter writer,TablutState state)
        {
            writer.Write(state.GetType().FullName);
            state.OnWrite(writer);
        }
        protected virtual void OnWrite(BinaryWriter writer) { }
        protected virtual void OnRead(BinaryReader reader) { }
    }
}
