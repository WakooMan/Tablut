using System;
using System.Collections.Generic;
using System.Reflection;
using Tablut.ViewModel;
using Xamarin.Forms;

namespace Tablut
{
    public class ApplicationState
    {
        Dictionary<Type, ConstructorInfo> PageCtrForVMs = new Dictionary<Type, ConstructorInfo>();
        public Page Page { get; private set; }
        public ApplicationViewModel Model { get; private set; }
        public ApplicationState(ApplicationViewModel model)
        {
            PageCtrForVMs.Add(typeof(GameViewModel), typeof(GamePage).GetConstructor(new Type[] { }));
            PageCtrForVMs.Add(typeof(InitGameViewModel), typeof(InitGamePage).GetConstructor(new Type[] { }));
            PageCtrForVMs.Add(typeof(MainMenuViewModel), typeof(MainMenuPage).GetConstructor(new Type[] { }));
            Model = model;
            Page = (Page)PageCtrForVMs[Model.GetType()].Invoke(new object[] { });
        }

        public void OnPushState(ApplicationViewModel viewModel)
        {
            Model = viewModel;
            Page = (Page)PageCtrForVMs[Model.GetType()].Invoke(new object[] { });
        }
    }
}