using System;
using System.Collections.Generic;
using System.Reflection;
using Tablut.ViewModel;
using Xamarin.Forms;

namespace Tablut
{
    public class ApplicationState
    {
        Dictionary<Type, Type> PageCtrForVMs = new Dictionary<Type, Type>();
        public Page Page { get; private set; }
        public ApplicationViewModel Model { get; private set; }
        public ApplicationState(ApplicationViewModel model)
        {
            PageCtrForVMs.Add(typeof(GameViewModel), typeof(GamePage));
            PageCtrForVMs.Add(typeof(InitGameViewModel), typeof(InitGamePage));
            PageCtrForVMs.Add(typeof(MainMenuViewModel), typeof(MainMenuPage));
            PageCtrForVMs.Add(typeof(LoadGameViewModel), typeof(LoadGamePage));
            Model = model;
            Page = (Page)Activator.CreateInstance(PageCtrForVMs[Model.GetType()]);
            Page.BindingContext = Model;
        }

        public void OnPushState(ApplicationViewModel viewModel)
        {
            Model = viewModel;
            Page = (Page)Activator.CreateInstance(PageCtrForVMs[Model.GetType()]);
            Page.BindingContext = Model;
        }
    }
}