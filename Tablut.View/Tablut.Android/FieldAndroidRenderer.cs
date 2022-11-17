using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using System;
using System.ComponentModel;
using Tablut;
using Tablut.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.FastRenderers;
using View = Android.Views.View;

[assembly:ExportRenderer(typeof(PieceView), typeof(FieldAndroidRenderer))]
namespace Tablut.Droid
{

    public class FieldAndroidRenderer : View, IVisualElementRenderer, IViewRenderer
    {
        private PieceView _element;

        private int? _defaultLabelFor;
        private VisualElementRenderer _visualElementRenderer;
        private VisualElementTracker _visualElementTracker;

        private Paint _attackerSoldierPaint;
        private ShapeDrawable _attackerSoldierShape;
        private Paint _defenderSoldierPaint;
        private ShapeDrawable _defenderSoldierShape;
        private Paint _selectedAttackerSoldierPaint;
        private ShapeDrawable _selectedAttackerSoldierShape;
        private Paint _selectedDefenderSoldierPaint;
        private ShapeDrawable _selectedDefenderSoldierShape;
        private Paint _defenderKingPaint;
        private ShapeDrawable _defenderKingShape;
        private Paint _selectedDefenderKingPaint;
        private ShapeDrawable _selectedDefenderKingShape;
        public FieldAndroidRenderer(Context context) : base(context)
            => _visualElementRenderer = new VisualElementRenderer(this);
        public PieceView Element
        {
            get => _element;
            set
            {
                if (value == _element)
                    return;

                PieceView oldElement = _element;
                _element = value;
                //OnElementChanged(new ElementChangedEventArgs<PieceView>(oldElement, _element));
            }
        }

        public VisualElementTracker Tracker => throw new NotImplementedException();

        public ViewGroup ViewGroup => throw new NotImplementedException();

        public View View => throw new NotImplementedException();

        VisualElement IVisualElementRenderer.Element => Element;

        public event EventHandler<VisualElementChangedEventArgs> ElementChanged;
        public event EventHandler<PropertyChangedEventArgs> ElementPropertyChanged;

        public Xamarin.Forms.SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            throw new NotImplementedException();
        }

        public void MeasureExactly()
        {
            throw new NotImplementedException();
        }

        public void SetElement(Xamarin.Forms.VisualElement element)
        {
            throw new NotImplementedException();
        }

        public void SetLabelFor(int? id)
        {
            throw new NotImplementedException();
        }

        public void UpdateLayout()
        {
            throw new NotImplementedException();
        }
    }
}