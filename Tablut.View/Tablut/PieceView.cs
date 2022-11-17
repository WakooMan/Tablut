using System;
using System.Collections.Generic;
using System.Text;
using Tablut.ViewModel;
using Xamarin.Forms;

namespace Tablut
{
    public class PieceView : View
    {
        public static readonly BindableProperty PieceTypeProperty = BindableProperty.Create("PieceType", typeof(PieceType), typeof(PieceView), PieceType.None, BindingMode.OneWay);
        public PieceType PieceType
        {
            get => (PieceType)GetValue(PieceTypeProperty);
            set => SetValue(PieceTypeProperty, value);
        }
    }
}
