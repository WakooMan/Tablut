using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.Lifecycle;
using System;
using System.ComponentModel;
using Tablut;
using Tablut.Droid;
using Tablut.ViewModel;
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
        public event EventHandler<VisualElementChangedEventArgs> ElementChanged;
        public event EventHandler<PropertyChangedEventArgs> ElementPropertyChanged;
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
                OnElementChanged(new ElementChangedEventArgs<PieceView>(oldElement, _element));
            }
        }

        VisualElement IVisualElementRenderer.Element => Element;
        public VisualElementTracker Tracker => _visualElementTracker;
        public ViewGroup ViewGroup => null;
        public Android.Views.View View => this;
        private void OnElementChanged(ElementChangedEventArgs<PieceView> e)
        {
            if (e.OldElement != null)
            {
                //Unsubscribe from events
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;
            }

            if (e.NewElement != null)
            {
                this.EnsureId();

                //Subscribe to events
                e.NewElement.PropertyChanged += OnElementPropertyChanged;

                ElevationHelper.SetElevation(this, e.NewElement);

                SetLayerType(LayerType.Software, null);
                /*
                 * rajzolás
                _flagPaint = new Paint();
                _flagPaint.SetStyle(Paint.Style.FillAndStroke);
                _flagPaint.SetARGB(255, 255, 0, 0);
                _flagShape = new ShapeDrawable(new RectShape());
                _flagShape.Paint.Set(_flagPaint);

                _bombPaint = new Paint();
                _bombPaint.SetStyle(Paint.Style.Fill);
                _bombPaint.SetARGB(255, 0, 0, 0);
                _bombShape = new ShapeDrawable(new OvalShape());
                _bombShape.Paint.Set(_bombPaint);
                */
            }

            ElementChanged?.Invoke(this, new VisualElementChangedEventArgs(e.OldElement, e.NewElement));
            PostInvalidate();
        }
        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ElementPropertyChanged?.Invoke(this, e);

            switch (e.PropertyName)
            {
                case "PieceType": PostInvalidate(); break;
            }
        }

        #region Protected Methods

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            if (_element != null)
            {
                int centerX = canvas.Width / 2;
                int centerY = canvas.Height / 2;

                switch (_element.PieceType)
                {
                    case PieceType.SelectedAttackerSoldier:
                    case PieceType.AttackerSoldier:
                        break;
                    case PieceType.SelectedDefenderSoldier:
                    case PieceType.DefenderSoldier:
                        break;
                    case PieceType.SelectedDefenderKing:
                    case PieceType.DefenderKing:
                        break;
                    case PieceType.None:
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Public Methods

        //IVisualElementRenderer Implementation
        SizeRequest IVisualElementRenderer.GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            Measure(widthConstraint, heightConstraint);
            SizeRequest result = new SizeRequest(new Size(MeasuredWidth, MeasuredHeight), new Size(Context.ToPixels(20), Context.ToPixels(20)));
            return result;
        }
        public void SetElement(VisualElement element)
        {
            if (!(element is PieceView cellIcon))
            {
                throw new ArgumentException($"{nameof(element)} must be of type {nameof(PieceView)}");
            }

            if (_visualElementTracker == null)
                _visualElementTracker = new VisualElementTracker(this);
            Element = cellIcon;
        }
        public void SetLabelFor(int? id)
        {
            if (_defaultLabelFor == null)
                _defaultLabelFor = LabelFor;
            LabelFor = (int)(id ?? _defaultLabelFor);
        }
        public void UpdateLayout() => _visualElementTracker?.UpdateLayout();
        //End of IVisualElementRenderer Implementation


        public void MeasureExactly() => MeasureExactly(this, Element, Context);
        //IViewRenderer Implementation
        public void MeasureExactly(Android.Views.View control, VisualElement element, Context context)
        {
            if (control == null || _element == null)
                return;

            double width = _element.Width;
            double height = _element.Height;

            if (width <= 0 || height <= 0)
                return;

            int realWidth = (int)context.ToPixels(width);
            int realHeight = (int)context.ToPixels(height);

            int widthMeasureSpec = MeasureSpecFactory.MakeMeasureSpec(realWidth, MeasureSpecMode.Exactly);
            int heightMeasureSpec = MeasureSpecFactory.MakeMeasureSpec(realHeight, MeasureSpecMode.Exactly);

            control.Measure(widthMeasureSpec, heightMeasureSpec);
        }
        //End of IViewRenderer Implementation

        #endregion

        static class MeasureSpecFactory
        {
            public static int GetSize(int measureSpec)
            {
                const int modeMask = 0x3 << 30;
                return measureSpec & ~modeMask;
            }

            public static int MakeMeasureSpec(int size, MeasureSpecMode mode) => size + (int)mode;

        }
    }
}