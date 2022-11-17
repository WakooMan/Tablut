using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tablut.DI;
using Windows.UI.Xaml;

namespace Tablut.UWP
{
    public class WindowsHelper : INativeHelper
    {
        public void CloseApplication()
        {
            Application.Current.Exit();
        }
    }
}
