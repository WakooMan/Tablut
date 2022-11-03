using Tablut.Persistence;
using Xamarin.Forms;

namespace Tablut.ViewModel
{
    public class LoadGameViewModel : ApplicationViewModel
    {
        public string LoadGameText => "Load Game";
        public DelegateCommand LoadGameCommand { get; }
        public LoadGameViewModel()
        {
            LoadGameCommand = new DelegateCommand(Command_LoadGame);
        }

        private async void Command_LoadGame(object param)
        {
            TablutState state = await DependencyService.Get<ITablutPersistence>().LoadGameStateAsync("valami.tablut");
            OnPushState?.Invoke(state.Model);
        }
    }
}