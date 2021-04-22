using BluetoothXPlatformChat.Common.Interfaces;
using BluetoothXPlatformChat.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BluetoothXPlatformChat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceivePage
    {
        public ReceivePage()
        {
            IReceiverBluetoothService service = DependencyService.Get<IReceiverBluetoothService>();
            ReceiveViewModel viewModel = new ReceiveViewModel(service);
            this.BindingContext = viewModel;
            InitializeComponent();
        }
    }
}