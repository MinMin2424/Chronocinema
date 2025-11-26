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

namespace Chronocinema.UserControls
{
    /// <summary>
    /// Interaction logic for RatingUserControl.xaml
    /// </summary>
    public partial class RatingUserControl : UserControl
    {
        public RatingUserControl()
        {
            InitializeComponent();
        }

        public double Rating
        {
            get {  return (double)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }

        public static readonly DependencyProperty RatingProperty =
            DependencyProperty.Register(
                nameof(Rating), 
                typeof(double), 
                typeof(RatingUserControl)
            );

        public event RoutedEventHandler RatingChanged;

        public static readonly RoutedEvent RatingChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(RatingChanged), 
                RoutingStrategy.Bubble, 
                typeof(RoutedEventHandler),
                typeof(RatingUserControl)
            );
    }
}
