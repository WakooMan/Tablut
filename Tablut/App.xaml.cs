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
            ApplicationViewModel.SetOnPushState(async (viewModel) =>
            {
                CurrentState.OnPushState(viewModel);
                MainPage = CurrentState.Page;
            });
            CurrentState = new ApplicationState(new MainMenuViewModel());
            MainPage = CurrentState.Page;
        }
       

        protected override void OnStart()
        {
            //Task.Run(() =>  CurrentState.LoadApplicationState());
        }

        protected override void OnSleep()
        {
            Task.Run(() => CurrentState.SaveApplicationState());
        }

        protected override void OnResume()
        {
            Task.Run(() => CurrentState.LoadApplicationState());
        }
    }
}
