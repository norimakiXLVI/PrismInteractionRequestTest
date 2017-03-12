using Microsoft.Practices.Unity;
using Prism.Unity;
using PrismInteractionRequestTest.Views;
using System.Windows;

namespace PrismInteractionRequestTest
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
