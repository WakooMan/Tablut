using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Tablut.Model.GameModel;
using Tablut.Persistence;
using Tablut.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.GTKSpecific;
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace Tablut
{
    public class ApplicationState
    {
        Dictionary<Type,Type> PageForVMs = new Dictionary<Type, Type>();
        Dictionary<Type, ConstructorInfo> AppStateForVMs = new Dictionary<Type, ConstructorInfo>();
        public Page Page { get; private set; }
        public NavigationPage NavPage { get; }
        public ApplicationViewModel Model { get; private set; }
        public ApplicationState(ApplicationViewModel model)
        {
            PageForVMs.Add(typeof(GameViewModel), typeof(GamePage));
            PageForVMs.Add(typeof(InitGameViewModel), typeof(InitGamePage));
            PageForVMs.Add(typeof(MainMenuViewModel), typeof(MainMenuPage));
            PageForVMs.Add(typeof(LoadGameViewModel), typeof(LoadGamePage));
            PageForVMs.Add(typeof(GameMenuViewModel), typeof(GameMenuPage));
            PageForVMs.Add(typeof(GameOverViewModel), typeof(GameOverPage));
            AppStateForVMs.Add(typeof(GameViewModel), typeof(GameplayState).GetConstructor(new Type[] { typeof(GameViewModel)}));
            AppStateForVMs.Add(typeof(GameMenuViewModel), typeof(GameplayState).GetConstructor(new Type[] { typeof(GameMenuViewModel) }));
            AppStateForVMs.Add(typeof(GameOverViewModel), typeof(GameplayState).GetConstructor(new Type[] { typeof(GameOverViewModel) }));
            AppStateForVMs.Add(typeof(InitGameViewModel), typeof(InitGameState).GetConstructor(new Type[] { typeof(InitGameViewModel) }));
            AppStateForVMs.Add(typeof(LoadGameViewModel), typeof(LoadGameState).GetConstructor(new Type[] { typeof(LoadGameViewModel) }));
            Model = model;
            Page = (Page)Activator.CreateInstance(PageForVMs[Model.GetType()]);
            Page.BindingContext = Model;
            NavPage = new NavigationPage(Page);
        }

        public async Task SaveApplicationState()
        {
            if (AppStateForVMs.ContainsKey(Model.GetType()))
            {
                await DependencyService.Get<ITablutPersistence>().SaveGameState("save.dat", (TablutState)AppStateForVMs[Model.GetType()].Invoke(new object[] { Model }));
            }
            else if(Model.GetType() == typeof(MainMenuViewModel))
            {
                string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "save.dat");
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
            }
        }

        public async Task LoadApplicationState()
        {
            TablutState state = DependencyService.Get<ITablutPersistence>().LoadGameState("save.dat");
            if (state != null)
            {
                await OnPushState(state.Model);
                if (state.Model is GameViewModel)
                {
                    GameViewModel model = (GameViewModel)state.Model;
                    switch (model.Model.GameState)
                    {
                        case GameState.Paused:
                            await OnPushState(new GameMenuViewModel(model));
                            break;
                        case GameState.AttackerWon:
                            await OnPushState(new GameOverViewModel(model.AttackerName,model,PlayerSide.Attacker));
                            break;
                        case GameState.DefenderWon:
                            await OnPushState(new GameOverViewModel(model.DefenderName, model, PlayerSide.Defender));
                            break;
                    }
                }
            }
        }

        public async Task OnPushState(ApplicationViewModel viewModel)
        {
            Model = viewModel;
            Page = (Page)Activator.CreateInstance(PageForVMs[Model.GetType()]);
            Page.BindingContext = Model;
            NavigationPage.SetHasBackButton(Page, false);
            await NavPage.PushAsync(Page);
        }

        public async Task OnPopState()
        {
            await NavPage.PopAsync();
        }

        public async Task OnPopToRootState()
        {
            await NavPage.PopToRootAsync();
        }

    }
}