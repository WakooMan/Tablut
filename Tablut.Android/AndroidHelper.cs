using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tablut.DI;
using Xamarin.Forms;

namespace Tablut.Droid
{
    public class AndroidHelper : INativeHelper
    {
        [Obsolete]
        public void CloseApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}