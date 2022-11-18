using System;
using System.Collections.Generic;
using System.ComponentModel;
using Tablut.UWP;
using Tablut;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Forms.Platform.UWP;
using Tablut.ViewModel;
using Windows.UI.Xaml.Controls;

[assembly: ExportRenderer(typeof(PieceView), typeof(PieceUWPRenderer))]
namespace Tablut.UWP
{ 
    public class PieceUWPRenderer: ViewRenderer<PieceView, Image>
    {

        private Image _image;

        private Dictionary<PieceType, BitmapImage> Images;

        private void UpdateImageSource(PieceType pieceType)
        {
            _image.Source = Images[pieceType];
        }

        protected override void OnElementChanged(ElementChangedEventArgs<PieceView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    _image = new Image();
                    if (Images == null)
                    {
                        Images = new Dictionary<PieceType, BitmapImage>();
                        Images.Add(PieceType.None,null);
                        Images.Add(PieceType.DefenderKing, new BitmapImage(new Uri("ms-appx:///Assets/DefenderKing.png")));
                        Images.Add(PieceType.DefenderSoldier, new BitmapImage(new Uri("ms-appx:///Assets/DefenderSoldier.png")));
                        Images.Add(PieceType.SelectedDefenderKing, new BitmapImage(new Uri("ms-appx:///Assets/SelectedDefenderKing.png")));
                        Images.Add(PieceType.SelectedDefenderSoldier, new BitmapImage(new Uri("ms-appx:///Assets/SelectedDefenderSoldier.png")));
                        Images.Add(PieceType.AttackerSoldier, new BitmapImage(new Uri("ms-appx:///Assets/AttackerSoldier.png")));
                        Images.Add(PieceType.SelectedAttackerSoldier, new BitmapImage(new Uri("ms-appx:///Assets/SelectedAttackerSoldier.png")));
                    }
                    _image.Source = Images[e.NewElement.PieceType];
                    SetNativeControl(_image);
                }
                //Subscribe to events
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (_image != null)
            {
                PieceView element = sender as PieceView;
                switch (e.PropertyName)
                {
                    case "PieceType": UpdateImageSource(element.PieceType); break;
                }
            }
        }
    }
}
