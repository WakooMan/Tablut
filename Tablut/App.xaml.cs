using System;
using Tablut.ViewModel;
using Tablut.Model.GameModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tablut
{
    public partial class App : Application
    {
        private NavigationPage _rootPage;
        private ApplicationState CurrentState;
        public App()
        {
            InitializeComponent();
            ApplicationViewModel.OnPushState = async (state) =>
            {
                CurrentState = state;
                await _rootPage.Navigation.PushAsync(CurrentState.Page);
                _rootPage.BindingContext = CurrentState.Model;
            };
            CurrentState = new ApplicationState(new MainPage(), new GameViewModel("Viktor", "Viktória"));
            _rootPage = new NavigationPage(CurrentState.Page);
            _rootPage.BindingContext = CurrentState.Model;
            MainPage = _rootPage;
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
