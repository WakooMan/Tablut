using System;
using Tablut.ViewModel;
using Tablut.Model.GameModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tablut
{
    public partial class App : Application
    {
        private GameModel _model;
        private GameViewModel _viewModel;
        private NavigationPage _rootPage;
        public App()
        {
            InitializeComponent();
            _model = new GameModel("Viktor","Viktória");
            _viewModel = new GameViewModel(_model);
            _rootPage = new NavigationPage(new MainPage());
            _rootPage.BindingContext = _viewModel;
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
