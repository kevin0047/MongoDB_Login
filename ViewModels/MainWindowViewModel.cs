using Prism.Commands;
using Prism.Mvvm;

using System.Windows;

namespace MongoDB_Login.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private Visibility _isButtonVisible = Visibility.Visible;
        public Visibility IsButtonVisible
        {
            get { return _isButtonVisible; }
            set { SetProperty(ref _isButtonVisible, value); }
        }

        public DelegateCommand NavigateToLoginCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateToLoginCommand = new DelegateCommand(NavigateToLogin);
        }

        private void NavigateToLogin()
        {
            IsButtonVisible = Visibility.Collapsed;
            _regionManager.RequestNavigate("ContentRegion", "LoginView");
        }
    }
}
