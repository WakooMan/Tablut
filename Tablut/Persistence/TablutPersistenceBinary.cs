using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Tablut.Persistence
{
    public class TablutPersistenceBinary : ITablutPersistence
    {
        public async Task<TablutState> LoadGameStateAsync(string filename)
        {
            try
            {
                TablutState state = null;
                await Task.Run(() =>
                {
                    using (FileStream stream = new FileStream(filename, FileMode.Open))
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        state = TablutState.Read(reader);
                    }
                });
                return state;
            }
            catch { return null; }
        }

        public async Task SaveGameStateAsync(string fileName,TablutState state)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        TablutState.Write(bw, state);
                    }
                });
            }
            catch { }
        }
    }
}
