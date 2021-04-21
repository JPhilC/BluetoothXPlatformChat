using BluetoothXPlatformChat.Services;
using Xamarin.Forms;

namespace BluetoothXPlatformChat
{
    public partial class App
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
