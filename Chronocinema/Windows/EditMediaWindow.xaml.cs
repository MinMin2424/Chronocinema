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
    /// Interaction logic for EditMediaWindow.xaml
    /// </summary>
    public partial class EditMediaWindow : Window
    {
        public EditViewModel ViewModel { get; }
        public EditMediaWindow(MediaItem mediaItem)
        {
            InitializeComponent();

            ViewModel = new EditViewModel(mediaItem);
            DataContext = ViewModel;
        }
    }
}
