
namespace Tablut.ViewModel
{
    public class SavedGameViewModel: BindingSource
    {
        public string FileName { get; }
        public DelegateCommand LoadGameCommand { get; }
        public SavedGameViewModel(string fileName,DelegateCommand loadGameCommand)
        {
            FileName = fileName;
            LoadGameCommand = loadGameCommand;
        }
    }
}