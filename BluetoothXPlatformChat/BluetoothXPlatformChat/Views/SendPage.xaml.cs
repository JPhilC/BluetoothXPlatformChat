using System.IO;
using BluetoothXPlatformChat.Common;
using BluetoothXPlatformChat.Common.Interfaces;
using BluetoothXPlatformChat.ViewModels;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BluetoothXPlatformChat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendPage
    {
        private SendViewModel _viewModel;
        private ISenderBluetoothService _service;
        public SendPage()
        {
            _service = DependencyService.Get<ISenderBluetoothService>();
            _viewModel = new SendViewModel(_service);
            this.BindingContext = _viewModel;
            InitializeComponent();
        }

        //protected override void OnAppearing()
        //{
        //    BluetoothRadio radio = BluetoothRadio.Default;
        //    if (radio != null && radio.Mode == RadioMode.PowerOff)
        //    {
        //        BluetoothRadio.Default.Mode = RadioMode.Connectable;
        //    }

        //    BluetoothDeviceInfo device = null;

        //    using (BluetoothClient bluetoothClient = new BluetoothClient())
        //    {

        //        var devices = bluetoothClient.PairedDevices;

        //        foreach (BluetoothDeviceInfo bdi in devices)
        //        {
        //            System.Diagnostics.Debug.WriteLine(bdi.DeviceName + " " + bdi.DeviceAddress.ToString("C") + " " +
        //                                               bdi.Authenticated);
        //        }

        //        foreach (BluetoothDeviceInfo bdi in bluetoothClient.DiscoverDevices(24))
        //        {
        //            System.Diagnostics.Debug.WriteLine(bdi.DeviceName + " " + bdi.DeviceAddress.ToString("C") + " " +
        //                                               bdi.Authenticated);
        //            if (device == null)
        //                device = bdi;
        //        }

        //        if (device != null)
        //        {

        //            bluetoothClient.Connect(device.DeviceAddress, Constants.ServiceClassId);
        //            _bluetoothStream = bluetoothClient.GetStream();
        //            if (bluetoothClient.Connected && _bluetoothStream != null)
        //            {
        //                // write the data in the stream
        //                var buffer = System.Text.Encoding.UTF8.GetBytes("Hello world!");
        //                _bluetoothStream.Write(buffer, 0, buffer.Length);
        //                _bluetoothStream.Flush();
        //                _bluetoothStream.Close();
        //            }

        //        }
        //    }

        //    base.OnAppearing();
        //}

        //Stream _bluetoothStream;

        //protected override void OnDisappearing()
        //{
        //    if (_bluetoothStream != null)
        //    {
        //        _bluetoothStream.Dispose();
        //        _bluetoothStream = null;
        //    }
        //    base.OnDisappearing();
        //}

    }

}