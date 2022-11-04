using System;
using System.Collections.ObjectModel;
using System.IO;
using Tablut.Persistence;
using Xamarin.Forms;

namespace Tablut.ViewModel
{
    public class LoadGameViewModel : ApplicationViewModel
    {
        public string TitleText => "Load Game";
        public ObservableCollection<SavedGameViewModel> SavedGames { get; } = new ObservableCollection<SavedGameViewModel>();
        public LoadGameViewModel()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            foreach (string filepath in Directory.GetFiles(path))
            {
                if (Path.GetExtension(filepath) == ".tablut")
                {
                    SavedGames.Add(new SavedGameViewModel(Path.GetFileNameWithoutExtension(filepath),new DelegateCommand(Command_LoadGame)));
                }
            }
        }

        private void Command_LoadGame(object param)
        {
            SavedGameViewModel model = param as SavedGameViewModel;
            TablutState state = DependencyService.Get<ITablutPersistence>().LoadGameState(model.FileName + ".tablut");
            OnPushState?.Invoke(state.Model);
        }
    }
}