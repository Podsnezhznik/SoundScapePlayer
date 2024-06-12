using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Test1_Base
{
    public partial class IntervalSlider : UserControl
    {
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(IntervalSlider), new UIPropertyMetadata(0d));
        public static readonly DependencyProperty LowerValueProperty =
            DependencyProperty.Register("LowerValue", typeof(double), typeof(IntervalSlider), new UIPropertyMetadata(0d, null, LowerValueCoerceValueCallback));
        public static readonly DependencyProperty UpperValueProperty =
            DependencyProperty.Register("UpperValue", typeof(double), typeof(IntervalSlider), new UIPropertyMetadata(1d, null, UpperValueCoerceValueCallback));
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(IntervalSlider), new UIPropertyMetadata(1d));
        public static readonly DependencyProperty IsSnapToTickEnabledProperty =
            DependencyProperty.Register("IsSnapToTickEnabled", typeof(bool), typeof(IntervalSlider), new UIPropertyMetadata(false));
        public static readonly DependencyProperty TickFrequencyProperty =
            DependencyProperty.Register("TickFrequency", typeof(double), typeof(IntervalSlider), new UIPropertyMetadata(0.1d));
        public static readonly DependencyProperty TickPlacementProperty =
            DependencyProperty.Register("TickPlacement", typeof(TickPlacement), typeof(IntervalSlider), new UIPropertyMetadata(TickPlacement.None));
        public static readonly DependencyProperty TicksProperty =
            DependencyProperty.Register("Ticks", typeof(DoubleCollection), typeof(IntervalSlider), new UIPropertyMetadata(null));

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public double LowerValue
        {
            get { return (double)GetValue(LowerValueProperty); }
            set { SetValue(LowerValueProperty, value); }
        }

        public double UpperValue
        {
            get { return (double)GetValue(UpperValueProperty); }
            set { SetValue(UpperValueProperty, value); }
        }

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public bool IsSnapToTickEnabled
        {
            get { return (bool)GetValue(IsSnapToTickEnabledProperty); }
            set { SetValue(IsSnapToTickEnabledProperty, value); }
        }

        public double TickFrequency
        {
            get { return (double)GetValue(TickFrequencyProperty); }
            set { SetValue(TickFrequencyProperty, value); }
        }

        public TickPlacement TickPlacement
        {
            get { return (TickPlacement)GetValue(TickPlacementProperty); }
            set { SetValue(TickPlacementProperty, value); }
        }

        public DoubleCollection Ticks
        {
            get { return (DoubleCollection)GetValue(TicksProperty); }
            set { SetValue(TicksProperty, value); }
        }

        static IntervalSlider()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(IntervalSlider), new FrameworkPropertyMetadata(typeof(IntervalSlider)));
        }

        private static object LowerValueCoerceValueCallback(DependencyObject target, object valueObject)
        {
            IntervalSlider targetSlider = (IntervalSlider)target;
            double value = (double)valueObject;

            return Math.Min(value, targetSlider.UpperValue);
        }

        private static object UpperValueCoerceValueCallback(DependencyObject target, object valueObject)
        {
            IntervalSlider targetSlider = (IntervalSlider)target;
            double value = (double)valueObject;

            return Math.Max(value, targetSlider.LowerValue);
        }
    }
}
