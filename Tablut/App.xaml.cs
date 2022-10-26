using System;
using Tablut.Model.GameModel;
using Tablut.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tablut
{
    public partial class App : Application
    {
        private GameModel _model;
        private GameViewModel _viewModel;
        public App()
        {
            InitializeComponent();
            _model = new GameModel("Viktor","Viktória");
            _viewModel = new GameViewModel(_model);
            MainPage = new MainPage();
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
