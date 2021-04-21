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
        private string _data = "TestMessage";
        private Device _selectDevice;
        private string _resultValue;

        /// <summary>  
        /// Initializes a new instance of the <see cref="SenderViewModel"/> class.  
        /// </summary>  
        /// <param name="senderBluetoothService">  
        /// The Sender bluetooth service.  
        /// </param>  
        public SendViewModel(ISenderBluetoothService senderBluetoothService)
        {
            Title = "Send";
            _senderBluetoothService = senderBluetoothService;
            ResultValue = "N/D";
            SendCommand = new RelayCommand(
                SendData,
                () => !string.IsNullOrEmpty(Data) && SelectDevice != null && SelectDevice.DeviceInfo != null);
            Devices = new ObservableCollection<Device>
        {
            new Device(null) { DeviceName = "Searching..." }
        };
            Messenger.Default.Register<Message>(this, ShowDevice);
        }

        /// <summary>  
        /// Gets or sets the devices.  
        /// </summary>  
        /// <value>  
        /// The devices.  
        /// </value>  
        public ObservableCollection<Device> Devices
        {
            get; set;
        }

        /// <summary>  
        /// Gets or sets the select device.  
        /// </summary>  
        /// <value>  
        /// The select device.  
        /// </value>  
        public Device SelectDevice
        {
            get => _selectDevice;
            set
            {
                if (Set(ref _selectDevice, value))
                {
                    this.SendCommand.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>  
        /// Gets or sets the data.  
        /// </summary>  
        /// <value>  
        /// The data.  
        /// </value>  
        public string Data
        {
            get => _data;
            set => Set(ref _data, value);
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

        /// <summary>  
        /// Gets the send command.  
        /// </summary>  
        /// <value>  
        /// The send command.  
        /// </value>  
        public RelayCommand SendCommand { get; private set; }

        private async void SendData()
        {
            ResultValue = "N/D";
            var wasSent = await _senderBluetoothService.Send(SelectDevice, Data);
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
                Data = string.Empty;
            }
        }
    }
}
