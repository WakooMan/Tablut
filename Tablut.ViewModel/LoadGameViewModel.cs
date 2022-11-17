using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;

namespace Tablut.ViewModel
{
    public class LoadGameViewModel : ApplicationViewModel
    {
        public string TitleText => "Load Game";
        public string BackText => "Go Back To Menu";
        public DelegateCommand BackCommand { get; }
        public ObservableCollection<SavedGameViewModel> SavedGames { get; } = new ObservableCollection<SavedGameViewModel>();
        public LoadGameViewModel()
        {
            BackCommand = new DelegateCommand(Command_Back);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            foreach (string filepath in Directory.GetFiles(path))
            {
                if (Path.GetExtension(filepath) == ".tablut")
                {
                    SavedGames.Add(new SavedGameViewModel(Path.GetFileNameWithoutExtension(filepath),new DelegateCommand(Command_LoadGame)));
                }
            }
        }

        private void Command_Back(object param)
        {
            OnPushState?.Invoke(new MainMenuViewModel());
        }

        private void Command_LoadGame(object param)
        {
            SavedGameViewModel model = param as SavedGameViewModel;
            ApplicationViewModel newmodel = LoadGame?.Invoke(model.FileName + ".tablut");
            OnPushState?.Invoke(newmodel);
        }
    }
}