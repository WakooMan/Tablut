using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Tablut.Persistence
{
    public class TablutPersistenceBinary : ITablutPersistence
    {
        public Task<TablutState> LoadGameState(string fileName)
        {
            try
            {
                string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
                TablutState state = null;
                using (FileStream stream = new FileStream(savePath, FileMode.Open))
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    state = TablutState.Read(reader);
                }
                return Task.FromResult(state);
            }
            catch(Exception) { return Task.FromResult<TablutState>(null); }
        }

        public Task SaveGameState(string fileName,TablutState state)
        {
            try
            {
                string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
                using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    TablutState.Write(bw, state);
                }
                return Task.CompletedTask;
            }
            catch { return Task.CompletedTask; }
        }
    }
}
