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
    /// Interaction logic for LabelTextBox.xaml
    /// </summary>
    public partial class LabelTextBox : UserControl
    {
        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(LabelTextBox), new PropertyMetadata(default(string)));

        // Using a DependencyProperty as the backing store for LabelAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelAlignmentProperty =
            DependencyProperty.Register("LabelAlignment", typeof(HorizontalAlignment), typeof(LabelTextBox), new PropertyMetadata(default(HorizontalAlignment)));

        // Using a DependencyProperty as the backing store for Ratio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register("LabelWidth", typeof(GridLength), typeof(LabelTextBox), new PropertyMetadata(GridLength.Auto));

        // Using a DependencyProperty as the backing store for LabelMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelMarginProperty =
            DependencyProperty.Register("LabelMargin", typeof(Thickness), typeof(LabelTextBox), new PropertyMetadata(default(Thickness)));

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(LabelTextBox), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        // Using a DependencyProperty as the backing store for ValueReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueReadOnlyProperty =
            DependencyProperty.Register("ValueReadOnly", typeof(bool), typeof(LabelTextBox), new PropertyMetadata(default(bool)));

        // Using a DependencyProperty as the backing store for TextAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueAlignmentProperty =
            DependencyProperty.Register("ValueAlignment", typeof(TextAlignment), typeof(LabelTextBox), new PropertyMetadata(default(TextAlignment)));

        // Using a DependencyProperty as the backing store for ValueWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueWidthProperty =
            DependencyProperty.Register("ValueWidth", typeof(GridLength), typeof(LabelTextBox), new PropertyMetadata(GridLength.Auto));

        // Using a DependencyProperty as the backing store for ValueMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueMarginProperty =
            DependencyProperty.Register("ValueMargin", typeof(Thickness), typeof(LabelTextBox), new PropertyMetadata(default(Thickness)));

        public LabelTextBox()
        {
            InitializeComponent();
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public HorizontalAlignment LabelAlignment
        {
            get { return (HorizontalAlignment)GetValue(LabelAlignmentProperty); }
            set { SetValue(LabelAlignmentProperty, value); }
        }

        public GridLength LabelWidth
        {
            get { return (GridLength)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }

        public Thickness LabelMargin
        {
            get { return (Thickness)GetValue(LabelMarginProperty); }
            set { SetValue(LabelMarginProperty, value); }
        }

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public bool ValueReadOnly
        {
            get { return (bool)GetValue(ValueReadOnlyProperty); }
            set { SetValue(ValueReadOnlyProperty, value); }
        }

        public TextAlignment ValueAlignment
        {
            get { return (TextAlignment)GetValue(ValueAlignmentProperty); }
            set { SetValue(ValueAlignmentProperty, value); }
        }

        public GridLength ValueWidth
        {
            get { return (GridLength)GetValue(ValueWidthProperty); }
            set { SetValue(ValueWidthProperty, value); }
        }

        public Thickness ValueMargin
        {
            get { return (Thickness)GetValue(ValueMarginProperty); }
            set { SetValue(ValueMarginProperty, value); }
        }
    }
}
