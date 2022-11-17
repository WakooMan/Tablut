using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Tablut.DI;
using UIKit;

namespace Tablut.iOS
{
    public class IOSHelper : INativeHelper
    {
        public void CloseApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}