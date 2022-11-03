using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tablut.Persistence
{
    public interface ITablutPersistence
    {
        Task SaveGameStateAsync(string fileName,TablutState state);
        Task<TablutState> LoadGameStateAsync(string filename);
    }
}
