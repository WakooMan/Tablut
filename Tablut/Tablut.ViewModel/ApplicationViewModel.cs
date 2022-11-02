using System;

namespace Tablut.ViewModel
{
    public abstract class ApplicationViewModel: BindingSource
    {
        public static Action<ApplicationState> OnPushState;

        protected static void PushState(ApplicationState state) => OnPushState?.Invoke(state);
    }
}