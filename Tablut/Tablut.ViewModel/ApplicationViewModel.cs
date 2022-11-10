using System;

namespace Tablut.ViewModel
{
    public abstract class ApplicationViewModel: BindingSource
    {
        protected static Action<ApplicationViewModel> OnPushState = null;
        protected static Action OnPopState = null;
        protected static Action OnPopToRootState = null;

        public static void SetOnPushState(Action<ApplicationViewModel> onPushState)
        {
            if (OnPushState == null)
            {
                OnPushState = onPushState;
            }
        }

        public static void SetOnPopState(Action onPopState)
        {
            if (OnPopState == null)
            {
                OnPopState = onPopState;
            }
        }

        public static void SetOnPopToRootState(Action onPopToRootState)
        {
            if (OnPopToRootState == null)
            {
                OnPopToRootState = onPopToRootState;
            }
        }


    }
}