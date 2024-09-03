// ViewModels/RegisterViewModel.cs
using MongoDB_Login.Models;
using MongoDB_Login.Services;
using System.Windows;

namespace MongoDB_Login.ViewModels
{
    public class RegisterViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly UserService _userService;

        public string Username { get; set; }
        public string Password { get; set; }
        public string AdditionalInfo { get; set; }

        public DelegateCommand RegisterCommand { get; private set; }

        public RegisterViewModel(IRegionManager regionManager, UserService userService)
        {
            _regionManager = regionManager;
            _userService = userService;

            RegisterCommand = new DelegateCommand(async () => await Register());
        }

        private async Task Register()
        {
            var newUser = new User
            {
                Username = Username,
                Password = Password,
                AdditionalInfo = AdditionalInfo
            };

            var isCreated = await _userService.CreateUserAsync(newUser);
            if (isCreated)
            {
                MessageBox.Show("User successfully registered!");
                _regionManager.RequestNavigate("ContentRegion", "LoginView");
            }
            else
            {
                MessageBox.Show("Username already exists.");
            }
        }
    }
}
