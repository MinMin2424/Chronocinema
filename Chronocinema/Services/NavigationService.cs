using Chronocinema.Views;

namespace Chronocinema.Services
{
    public class NavigationService
    {
        private static NavigationService _instance;
        public static NavigationService Instance => _instance ??= new NavigationService();
        public event Action<object> ViewChanged;
        public event Action GoBackRequested;
        private Stack<object> _navigationBack = new Stack<object>();

        public void NavigateTo(object view)
        {
            if (_navigationBack.Count > 0)
            {
                _navigationBack.Push(view);
            }
            else
            {
                _navigationBack.Push(view);
            }
            ViewChanged?.Invoke(view);
        }

        public void GoBack()
        {
            if (_navigationBack.Count > 1)
            {
                _navigationBack.Pop();
                var previousView = _navigationBack.Peek();
                ViewChanged?.Invoke(previousView);
            }
            else if (_navigationBack.Count == 1)
            {
                var mainScreen = new MainScreen();
                NavigateTo(mainScreen);
            }
        }

        public void ClearHistory()
        {
            _navigationBack.Clear();
        }
    }
}
