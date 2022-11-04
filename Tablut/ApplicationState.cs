using System;
using System.Collections.Generic;
using System.Reflection;
using Tablut.Persistence;
using Tablut.ViewModel;
using Xamarin.Forms;

namespace Tablut
{
    public class ApplicationState
    {
        Dictionary<Type,Type> PageForVMs = new Dictionary<Type, Type>();
        Dictionary<Type, ConstructorInfo> AppStateForVMs = new Dictionary<Type, ConstructorInfo>();
        public Page Page { get; private set; }
        public ApplicationViewModel Model { get; private set; }
        public ApplicationState(ApplicationViewModel model)
        {
            PageForVMs.Add(typeof(GameViewModel), typeof(GamePage));
            PageForVMs.Add(typeof(InitGameViewModel), typeof(InitGamePage));
            PageForVMs.Add(typeof(MainMenuViewModel), typeof(MainMenuPage));
            PageForVMs.Add(typeof(LoadGameViewModel), typeof(LoadGamePage));
            AppStateForVMs.Add(typeof(GameViewModel), typeof(GameplayState).GetConstructor(new Type[] { typeof(GameViewModel)}));
            AppStateForVMs.Add(typeof(InitGameViewModel), typeof(InitGameState).GetConstructor(new Type[] { typeof(InitGameViewModel) }));
            AppStateForVMs.Add(typeof(MainMenuViewModel), typeof(MainMenuState).GetConstructor(new Type[] { typeof(MainMenuViewModel) }));
            AppStateForVMs.Add(typeof(LoadGameViewModel), typeof(LoadGameState).GetConstructor(new Type[] { typeof(LoadGameViewModel) }));
            Model = model;
            Page = (Page)Activator.CreateInstance(PageForVMs[Model.GetType()]);
            Page.BindingContext = Model;
        }

        public void SaveApplicationState()
        {
            DependencyService.Get<ITablutPersistence>().SaveGameState("save.dat", (TablutState)AppStateForVMs[Model.GetType()].Invoke(new object[] { Model }));
        }

        public void LoadApplicationState()
        {
            TablutState state = DependencyService.Get<ITablutPersistence>().LoadGameState("save.dat");
            if (state != null)
            {
                OnPushState(state.Model);
            }
        }

        public void OnPushState(ApplicationViewModel viewModel)
        {
            Model = viewModel;
            Page = (Page)Activator.CreateInstance(PageForVMs[Model.GetType()]);
            Page.BindingContext = Model;
        }
    }
}