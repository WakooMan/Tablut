using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tablut.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TableListView: TableView
    {
        public TableListView(): base()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(
         "ItemSource",
         typeof(IEnumerable<object>),
         typeof(TableListView),
         new object[] { });

        public IEnumerable<object> ItemSource
        {
            get => (IEnumerable<object>)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }
    }
}