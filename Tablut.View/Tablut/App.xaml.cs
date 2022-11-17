using System;
using Tablut.ViewModel;
using Tablut.Model.GameModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.IO;
using Tablut.DI;
using Tablut.Persistence;
using Tablut.Persistence.Persistence;

[assembly:Dependency(typeof(Tablut.Persistence.TablutPersistenceBinary))]

namespace Tablut
{
    public partial class App : Application
    {
        private ApplicationState CurrentState;
        public App()
        {
            InitializeComponent();
            ApplicationViewModel.ExitGame = () => DependencyService.Get<INativeHelper>()?.CloseApplication();
            ApplicationViewModel.LoadGame = (filename) => (DependencyService.Get<ITablutPersistence>().LoadGameState(filename)).Result.Model;
            ApplicationViewModel.SaveGame = (filename, viewmodel) => DependencyService.Get<ITablutPersistence>().SaveGameState(filename, TablutStateFactory.Create(viewmodel));

            CurrentState = new ApplicationState(page => MainPage = page,new MainMenuViewModel());
            ApplicationViewModel.SetOnPushState((viewModel) =>
            {
                CurrentState.OnPushState(viewModel);
            });
        }
       

        protected async override void OnStart()
        {
            await CurrentState.LoadApplicationState();
        }

        protected async override void OnSleep()
        {
            await CurrentState.SaveApplicationState();
        }

        protected override async void OnResume()
        {
            await CurrentState.LoadApplicationState();
        }
    }
}
