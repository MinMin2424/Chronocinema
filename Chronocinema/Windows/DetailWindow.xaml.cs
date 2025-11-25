using Chronocinema.Models;
using Chronocinema.ViewModels;
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
using System.Windows.Shapes;

namespace Chronocinema.Windows
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        public DetailViewModel ViewModel { get; }
        public DetailWindow(MediaItem mediaItem)
        {
            InitializeComponent();
            ViewModel = new DetailViewModel(mediaItem);
            DataContext = ViewModel;
        }
    }
}
