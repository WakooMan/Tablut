using System;

namespace Tablut.ViewModel
{
    public abstract class ApplicationViewModel: BindingSource
    {
        protected static Action<ApplicationViewModel> OnPushState = null;

        public static void SetOnPushState(Action<ApplicationViewModel> onPushState)
        {
            if (OnPushState == null)
            {
                OnPushState = onPushState;
            }
        }

        
    }
}