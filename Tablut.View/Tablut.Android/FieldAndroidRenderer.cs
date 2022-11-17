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

        private Paint _defenderPaint;
        private Paint _selectedPaint;
        private Paint _attackerPaint;
        private ShapeDrawable _defenderCircleShape;
        private ShapeDrawable _attackerCircleShape;
        private ShapeDrawable _selectedCircleShape;
        private ShapeDrawable _crownShape;
        private ShapeDrawable _selectedCrownShape;
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

                //in the center of 100x100 rectangle
                Path crownPath = new Path();
                crownPath.MoveTo(8,90);
                crownPath.LineTo(8,50);

                crownPath.LineTo(22, 10);
                crownPath.LineTo(36, 50);
                crownPath.LineTo(50, 10);
                crownPath.LineTo(64, 50);
                crownPath.LineTo(78, 10);
                crownPath.LineTo(92, 50);

                crownPath.LineTo(92, 90);
                crownPath.LineTo(8, 90);

                crownPath.Close();


                _attackerPaint = new Paint();
                _attackerPaint.SetStyle(Paint.Style.Fill);
                _attackerPaint.SetARGB(255,0,0,0);

                _defenderPaint = new Paint();
                _defenderPaint.SetStyle(Paint.Style.Fill);
                _defenderPaint.SetARGB(255, 255, 255, 255);

                _selectedPaint = new Paint();
                _selectedPaint.SetStyle(Paint.Style.Stroke);
                _selectedPaint.SetARGB(255, 0, 128, 0);

                _selectedCircleShape = new ShapeDrawable(new OvalShape());
                _selectedCircleShape.Paint.Set(_selectedPaint);

                _selectedCrownShape = new ShapeDrawable(new PathShape(crownPath, 100, 100));
                _selectedCrownShape.Paint.Set(_selectedPaint);

                _attackerCircleShape = new ShapeDrawable(new OvalShape());
                _attackerCircleShape.Paint.Set(_attackerPaint);

                _defenderCircleShape = new ShapeDrawable(new OvalShape());
                _defenderCircleShape.Paint.Set(_defenderPaint);

                _crownShape = new ShapeDrawable(new PathShape(crownPath,100,100));
                _crownShape.Paint.Set(_defenderPaint);
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
                int size = Math.Min(canvas.Width / 5 * 4, canvas.Height / 5 * 4);
                int strokeWidth = Math.Min(canvas.Width / 10*2, canvas.Height / 10*2);

                switch (_element.PieceType)
                {
                    case PieceType.SelectedAttackerSoldier:
                        _attackerCircleShape.SetBounds(centerX - size, centerY - size, centerX + size, centerY + size);
                        _selectedPaint.StrokeWidth = strokeWidth;
                        _selectedCircleShape.SetBounds(centerX - size - strokeWidth, centerY - size - strokeWidth, centerX + size + strokeWidth, centerY + size + strokeWidth);
                        _attackerCircleShape.Draw(canvas);
                        _selectedCircleShape.Draw(canvas);
                        break;
                    case PieceType.AttackerSoldier:
                        _attackerCircleShape.SetBounds(centerX - size, centerY - size, centerX + size, centerY + size);
                        _attackerCircleShape.Draw(canvas);
                        break;
                    case PieceType.SelectedDefenderSoldier:
                        _defenderCircleShape.SetBounds(centerX - size, centerY - size, centerX + size, centerY + size);
                        _selectedPaint.StrokeWidth = strokeWidth;
                        _selectedCircleShape.SetBounds(centerX - size -strokeWidth, centerY - size -strokeWidth, centerX + size +strokeWidth, centerY + size + strokeWidth);
                        _defenderCircleShape.Draw(canvas);
                        _selectedCircleShape.Draw(canvas);
                        break;
                    case PieceType.DefenderSoldier:
                        _defenderCircleShape.SetBounds(centerX - size, centerY - size, centerX + size, centerY + size);
                        _defenderCircleShape.Draw(canvas);
                        break;
                    case PieceType.SelectedDefenderKing:
                        _crownShape.SetBounds(0 + strokeWidth, 0 + strokeWidth, canvas.Width -strokeWidth, canvas.Height -strokeWidth);
                        _selectedCrownShape.SetBounds(0, 0, canvas.Width, canvas.Height);
                        _crownShape.Draw(canvas);
                        _selectedCrownShape.Draw(canvas);
                        break;
                    case PieceType.DefenderKing:
                        _crownShape.SetBounds(0 + strokeWidth, 0 + strokeWidth, canvas.Width - strokeWidth, canvas.Height - strokeWidth);
                        _crownShape.Draw(canvas);
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