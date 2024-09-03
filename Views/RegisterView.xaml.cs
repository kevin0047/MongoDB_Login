using System.Windows;
using System.Windows.Controls;
using MongoDB_Login.ViewModels;

namespace MongoDB_Login.Views
{
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
            // PasswordBox의 PasswordChanged 이벤트를 처리하여 ViewModel과의 데이터 바인딩을 수동으로 처리합니다.
            PasswordBox passwordBox = (PasswordBox)this.FindName("PasswordBox");
            passwordBox.PasswordChanged += OnPasswordChanged;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            // PasswordBox의 Password를 ViewModel에 반영합니다.
            if (this.DataContext is RegisterViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
