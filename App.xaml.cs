using MauiAppProyecto.MVVM.Models;
namespace MauiAppProyecto
{
    public partial class App : Application
    {
        public static Usuario AppUser;
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}