using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronocinema.ViewModels
{
    public class LocatorViewModel
    {
        private static LocatorViewModel _instance;
        public static LocatorViewModel Instance => _instance ??= new LocatorViewModel();
        public MainViewModel MainViewModel { get; } = new MainViewModel();
        public DetailViewModel DetailViewModel { get; set; }
        public EditViewModel EditViewModel { get; set; }
    }
}
