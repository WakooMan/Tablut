using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tablut.Persistence
{
    public interface ITablutPersistence
    {
        void SaveGameState(string fileName,TablutState state);
        TablutState LoadGameState(string filename);
    }
}
