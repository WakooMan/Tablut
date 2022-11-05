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
        private NavigationPage NavigationPage;
        public App()
        {
            InitializeComponent();
            CurrentState = new ApplicationState(new MainMenuViewModel());
            NavigationPage = new NavigationPage(CurrentState.Page);
            MainPage = NavigationPage;
            ApplicationViewModel.SetOnPushState(async (viewModel) =>
            {
                CurrentState.OnPushState(viewModel);
                await NavigationPage.PopToRootAsync();
                NavigationPage.SetHasBackButton(CurrentState.Page, false);
                await NavigationPage.PushAsync(CurrentState.Page);
            });
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
