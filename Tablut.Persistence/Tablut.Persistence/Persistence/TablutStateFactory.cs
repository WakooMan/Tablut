using System;
using System.Collections.Generic;
using System.Text;
using Tablut.ViewModel;

namespace Tablut.Persistence.Persistence
{
    public static class TablutStateFactory
    {
        public static TablutState Create(ApplicationViewModel model)
        {
            if (model is GameViewModel)
            {
                return new GameplayState((GameViewModel)model);
            }
            else if (model is GameMenuViewModel)
            {
                return new GameplayState(((GameMenuViewModel)model).Game);
            }
            else if (model is LoadGameViewModel)
            {
                return new LoadGameState((LoadGameViewModel)model);
            }
            else if (model is InitGameViewModel)
            {
                return new InitGameState((InitGameViewModel)model);
            }
            else if (model is SavingGameViewModel)
            {
                return new SaveGameState(((SavingGameViewModel)model).Game);
            }
            else
            {
                throw new ArgumentException($"Not Implemented Persistence for ViewModel type: {model.GetType()}");
            }
        }
    }
}
