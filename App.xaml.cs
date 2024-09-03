
using MongoDB_Login.Views;
using System.Windows;


namespace MongoDB_Login
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindowView>();
            containerRegistry.RegisterForNavigation<LoginView>();
            containerRegistry.RegisterForNavigation<RegisterView>();
            containerRegistry.RegisterSingleton<Services.UserService>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindowView>();
        }
    }
}