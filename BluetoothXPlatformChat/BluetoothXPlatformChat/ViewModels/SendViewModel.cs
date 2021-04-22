using System.Collections.ObjectModel;
using BluetoothXPlatformChat.Common.Interfaces;
using BluetoothXPlatformChat.Common.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace BluetoothXPlatformChat.ViewModels
{
    public class SendViewModel : BaseViewModel
    {
        private readonly ISenderBluetoothService _senderBluetoothService;
        private string _resultValue;

        public SendViewModel(ISenderBluetoothService senderBluetoothService)
        {
            Title = "Send";
            _senderBluetoothService = senderBluetoothService;
            ResultValue = "N/D";
            Devices = new ObservableCollection<Device>
            {
                new Device(null) { DeviceName = "Searching..." }
            };
            Messenger.Default.Register<Message>(this, ShowDevice);
        }

        public ObservableCollection<Device> Devices
        {
            get; set;
        }

        private Device _selectedDevice;
        public Device SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                if (Set(ref _selectedDevice, value))
                {
                    this.SendCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _data = "";
        public string Data
        {
            get => _data;
            set
            {
                if (Set(ref _data, value))
                {
                    SendCommand.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>  
        /// Gets or sets the result value.  
        /// </summary>  
        /// <value>  
        /// The result value.  
        /// </value>  
        public string ResultValue
        {
            get { return _resultValue; }
            set { Set(ref _resultValue, value); }
        }

        private RelayCommand _sendCommand;
        /// <summary>  
        /// Gets the send command.  
        /// </summary>  
        /// <value>  
        /// The send command.  
        /// </value>  
        public RelayCommand SendCommand
        {
            get
            {
                return _sendCommand
                    ?? (_sendCommand = new RelayCommand(
                        SendData,
                        () => !string.IsNullOrEmpty(Data) && SelectedDevice != null && SelectedDevice.DeviceInfo != null));
            }

        }

        private async void SendData()
        {
            ResultValue = "N/D";
            var wasSent = await _senderBluetoothService.Send(SelectedDevice, Data);
            if (wasSent)
            {
                ResultValue = "The data was sent.";
            }
            else
            {
                ResultValue = "The data was not sent.";
            }
        }

        /// <summary>  
        /// Shows the device.  
        /// </summary>  
        /// <param name="deviceMessage">The device message.</param>  
        private async void ShowDevice(Message deviceMessage)
        {
            if (deviceMessage.IsToShowDevices)
            {
                var items = await _senderBluetoothService.GetDevices();
                Devices.Clear();
                foreach (var item in items)
                {
                    Devices.Add(item);
                }
                SendCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
