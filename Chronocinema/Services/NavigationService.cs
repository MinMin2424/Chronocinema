namespace Chronocinema.Services
{
    public class NavigationService
    {
        private static NavigationService _instance;
        public static NavigationService Instance => _instance ??= new NavigationService();
        public event Action<object> ViewChanged;
        public event Action GoBackRequested;

        public void NavigateTo(object view)
        {
            ViewChanged?.Invoke(view);
        }

        public void GoBack()
        {
            GoBackRequested?.Invoke();
        }
    }
}
