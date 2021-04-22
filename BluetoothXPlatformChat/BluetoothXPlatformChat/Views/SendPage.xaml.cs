using BluetoothXPlatformChat.Common.Interfaces;
using BluetoothXPlatformChat.Common.Model;
using BluetoothXPlatformChat.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BluetoothXPlatformChat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendPage
    {
        public SendPage()
        {
            ISenderBluetoothService service = DependencyService.Get<ISenderBluetoothService>();
            SendViewModel viewModel = new SendViewModel(service);
            this.BindingContext = viewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Messenger.Default.Send(new Message(true));
        }
    }

}