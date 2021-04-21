using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluetoothXPlatformChat.Common.Interfaces;
using BluetoothXPlatformChat.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BluetoothXPlatformChat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceivePage : ContentPage
    {
        private IReceiverBluetoothService _service;
        private ReceiveViewModel _viewModel;

        public ReceivePage()
        {
            //_service = DependencyService.Get<IReceiverBluetoothService>();
            //_viewModel = new ReceiveViewModel(_service);

            InitializeComponent();
        }
    }
}