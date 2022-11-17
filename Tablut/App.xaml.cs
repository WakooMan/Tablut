using System;
using Tablut.ViewModel;
using Tablut.Model.GameModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.IO;

[assembly:Dependency(typeof(Tablut.Persistence.TablutPersistenceBinary))]

namespace Tablut
{
    public partial class App : Application
    {
        private ApplicationState CurrentState;
        public App()
        {
            InitializeComponent();
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
