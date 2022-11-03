using System;
using Tablut.ViewModel;
using Tablut.Model.GameModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
