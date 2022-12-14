using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tablut.Persistence
{
    public interface ITablutPersistence
    {
        Task SaveGameState(string fileName,TablutState state);
        Task<TablutState> LoadGameState(string filename);
    }
}
