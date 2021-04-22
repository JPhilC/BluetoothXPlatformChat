using BluetoothXPlatformChat.Common.Interfaces;
using Xamarin.Forms;

namespace BluetoothXPlatformChat
{
    public partial class App
    {

        public App()
        {
            InitializeComponent();
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

        protected override void CleanUp()
        {
            // The send and receive services are both disposable (they hold on to the bluetooth client)
            // so need cleaning up.
            IReceiverBluetoothService receiverService = DependencyService.Get<IReceiverBluetoothService>();
            receiverService.Dispose();
            base.CleanUp();
        }
    }
}
