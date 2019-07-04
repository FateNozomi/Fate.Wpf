using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fate.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for ImageControl.xaml
    /// </summary>
    public partial class ImageControl : UserControl
    {
        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(BitmapSource), typeof(ImageControl), new PropertyMetadata(default(BitmapSource), FitImageCallback));

        // Using a DependencyProperty as the backing store for Scale.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(double), typeof(ImageControl), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        // Using a DependencyProperty as the backing store for FitImageOnSourceUpdated.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FitImageOnSourceUpdatedProperty =
            DependencyProperty.Register("FitImageOnSourceUpdated", typeof(bool), typeof(ImageControl), new PropertyMetadata(false));

        // Using a DependencyProperty as the backing store for CrosshairStroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CrosshairStrokeProperty =
            DependencyProperty.Register("CrosshairStroke", typeof(Brush), typeof(ImageControl), new PropertyMetadata(default(Brush)));

        // Using a DependencyProperty as the backing store for CrosshairOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CrosshairOpacityProperty =
            DependencyProperty.Register("CrosshairOpacity", typeof(double), typeof(ImageControl), new PropertyMetadata(default(double)));

        // Using a DependencyProperty as the backing store for CrosshairThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CrosshairThicknessProperty =
            DependencyProperty.Register("CrosshairThickness", typeof(double), typeof(ImageControl), new PropertyMetadata(1d));

        private Point? _lastCenterPositionOnImage;
        private Point? _lastPanPosition;

        public ImageControl()
        {
            InitializeComponent();
        }

        public BitmapSource Image
        {
            get { return (BitmapSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public bool FitImageOnSourceUpdated
        {
            get { return (bool)GetValue(FitImageOnSourceUpdatedProperty); }
            set { SetValue(FitImageOnSourceUpdatedProperty, value); }
        }

        public Brush CrosshairStroke
        {
            get { return (Brush)GetValue(CrosshairStrokeProperty); }
            set { SetValue(CrosshairStrokeProperty, value); }
        }

        public double CrosshairOpacity
        {
            get { return (double)GetValue(CrosshairOpacityProperty); }
            set { SetValue(CrosshairOpacityProperty, value); }
        }

        public double CrosshairThickness
        {
            get { return (double)GetValue(CrosshairThicknessProperty); }
            set { SetValue(CrosshairThicknessProperty, value); }
        }

        private static void FitImageCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageControl imageControl = d as ImageControl;
            if (imageControl.FitImageOnSourceUpdated || imageControl._lastCenterPositionOnImage == null)
            {
                imageControl.FitImage();
            }
        }

        private void FitImage()
        {
            if (Image != null)
            {
                double ratioX = ScrollViewer.ActualWidth / Image.Width;
                double ratioY = ScrollViewer.ActualHeight / Image.Height;

                Scale = ratioX < ratioY ? ratioX : ratioY;
                ZoomSlider.Minimum = Scale;
            }
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FitImage();
        }

        private void ScrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point currentPosition = e.GetPosition(ScrollViewer);
            if (currentPosition.X <= ScrollViewer.ViewportWidth && currentPosition.Y <= ScrollViewer.ViewportHeight)
            {
                Mouse.Capture(ScrollViewer);
                _lastPanPosition = currentPosition;
            }
        }

        private void ScrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_lastPanPosition.HasValue)
            {
                return;
            }

            Point currentPosition = e.GetPosition(ScrollViewer);
            double dX = currentPosition.X - _lastPanPosition.Value.X;
            double dY = currentPosition.Y - _lastPanPosition.Value.Y;

            ScrollViewer.ScrollToHorizontalOffset(ScrollViewer.HorizontalOffset - dX);
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset - dY);

            _lastPanPosition = currentPosition;
        }

        private void ScrollViewer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ScrollViewer.ReleaseMouseCapture();
            _lastPanPosition = null;
            e.Handled = true;
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _lastCenterPositionOnImage = Mouse.GetPosition(ImageView);
            double delta = Scale;

            if (e.Delta > 0)
            {
                delta += ZoomSlider.TickFrequency;
                if (delta > ZoomSlider.Maximum)
                {
                    delta = ZoomSlider.Maximum;
                }
            }
            else if (e.Delta < 0)
            {
                delta -= ZoomSlider.TickFrequency;
                if (delta < ZoomSlider.Minimum)
                {
                    delta = ZoomSlider.Minimum;
                }
            }

            Scale = delta;
            e.Handled = true;
        }

        private void ScrollViewer_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Point currentMousePosition = e.GetPosition(ScrollViewer);
            if (currentMousePosition.X <= ScrollViewer.ViewportWidth && currentMousePosition.Y <= ScrollViewer.ViewportHeight)
            {
                _lastCenterPositionOnImage = Mouse.GetPosition(ImageView);

                if (Scale != ZoomSlider.Minimum)
                {
                    FitImage();
                }
                else
                {
                    Scale = 1;
                }
            }
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
            {
                Point? previousPositionOnImage = null;
                Point? currentPositionOnImage = null;

                if (_lastCenterPositionOnImage.HasValue)
                {
                    previousPositionOnImage = _lastCenterPositionOnImage;
                    if (ZoomSlider.IsVisible)
                    {
                        var centerOfViewport = new Point(ScrollViewer.ViewportWidth / 2, ScrollViewer.ViewportHeight / 2);
                        currentPositionOnImage = ScrollViewer.TranslatePoint(centerOfViewport, ImageView);
                    }
                    else
                    {
                        currentPositionOnImage = Mouse.GetPosition(ImageView);
                    }
                }

                if (previousPositionOnImage.HasValue)
                {
                    double displacementXInTargetPixels = currentPositionOnImage.Value.X - previousPositionOnImage.Value.X;
                    double displacementYInTargetPixels = currentPositionOnImage.Value.Y - previousPositionOnImage.Value.Y;

                    double multiplicatorX = e.ExtentWidth / ImageView.ActualWidth;
                    double multiplicatorY = e.ExtentHeight / ImageView.ActualHeight;

                    double newOffsetX = ScrollViewer.HorizontalOffset - (displacementXInTargetPixels * multiplicatorX);
                    double newOffsetY = ScrollViewer.VerticalOffset - (displacementYInTargetPixels * multiplicatorY);

                    if (double.IsNaN(newOffsetX) || double.IsNaN(newOffsetY))
                    {
                        return;
                    }

                    ScrollViewer.ScrollToHorizontalOffset(newOffsetX);
                    ScrollViewer.ScrollToVerticalOffset(newOffsetY);
                }
            }
        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = (Slider)sender;
            if (slider.IsVisible)
            {
                var centerOfViewport = new Point(ScrollViewer.ViewportWidth / 2, ScrollViewer.ViewportHeight / 2);
                _lastCenterPositionOnImage = ScrollViewer.TranslatePoint(centerOfViewport, ImageView);
            }
        }
    }
}
