using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tablut.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tablut
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FieldView : Frame
    {
        public static readonly BindableProperty CellProperty = BindableProperty.Create("Cell",typeof(FieldViewModel),typeof(FieldView),null,BindingMode.OneWay);
        
        public FieldViewModel Cell 
        {
            get => GetValue(CellProperty) as FieldViewModel;
            set => SetValue(CellProperty,value);
        }
        public FieldView()
        {
            InitializeComponent();
        }
    }
}