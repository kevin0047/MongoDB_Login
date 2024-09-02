using System.Windows;
using System.Windows.Controls;
using MongoDB_Login.ViewModels; 

namespace MongoDB_Login
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new MainViewModel();
            DataContext = viewModel;

            if (this.FindName("PasswordBox") is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged += (s, e) =>
                {
                    if (DataContext is MainViewModel vm)
                    {
                        vm.Password = passwordBox.Password;
                    }
                };
            }
        }
    }
}