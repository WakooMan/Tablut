using Tablut.ViewModel;
using Xamarin.Forms;

namespace Tablut
{
    public class ApplicationState
    {
        public Page Page { get; private set; }
        public ApplicationViewModel Model { get; private set; }
        public ApplicationState(Page page,ApplicationViewModel model)
        {
            Page = page;
            Model = model;
        }
    }
}