using System;
using Tablut.ViewModel;
using Tablut.Model.GameModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

[assembly:Dependency(typeof(Tablut.Persistence.TablutPersistenceBinary))]

namespace Tablut
{
    public partial class App : Application
    {
        private ApplicationState CurrentState;
        public App()
        {
            InitializeComponent();
            CurrentState = new ApplicationState(new MainMenuViewModel());
            MainPage = CurrentState.NavPage;
            ApplicationViewModel.SetOnPushState(async (viewModel) =>
            {
                await CurrentState.OnPushState(viewModel);
            });
            ApplicationViewModel.SetOnPopState(async () =>
            {
                await CurrentState.OnPopState();
            });
            ApplicationViewModel.SetOnPopToRootState(async () =>
            {
                await CurrentState.OnPopToRootState();
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

        protected async override void OnResume()
        {
            await CurrentState.LoadApplicationState();
        }
    }
}
