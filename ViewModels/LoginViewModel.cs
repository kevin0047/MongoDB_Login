using MongoDB_Login.Services;
using System.Windows;

namespace MongoDB_Login.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly UserService _userService;

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public DelegateCommand LoginCommand { get; private set; }
        public DelegateCommand NavigateToRegisterCommand { get; private set; }

        public LoginViewModel(IRegionManager regionManager, UserService userService)
        {
            _regionManager = regionManager;
            _userService = userService;

            LoginCommand = new DelegateCommand(async () => await Login());
            NavigateToRegisterCommand = new DelegateCommand(NavigateToRegister);
        }

        private async Task Login()
        {
            var user = await _userService.GetUserAsync(Username, Password);
            if (user != null)
            {
                MessageBox.Show("Login Successful!");
                // 추가 기능 구현 가능
            }
            else
            {
                MessageBox.Show("Invalid Username or Password.");
            }
        }

        private void NavigateToRegister()
        {
            _regionManager.RequestNavigate("ContentRegion", "RegisterView");
        }
    }
}
