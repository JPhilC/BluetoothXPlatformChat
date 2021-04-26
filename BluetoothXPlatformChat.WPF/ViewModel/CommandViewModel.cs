using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;
using BluetoothXPlatformChat.Common;
using BluetoothXPlatformChat.Common.Interfaces;
using BluetoothXPlatformChat.Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace BluetoothXPlatformChat.WPF.ViewModel
{
    public sealed class CommandViewModel : ViewModelBase
    {
        private readonly ICommandResponseBluetoothService _service;
        private string _data;
        private Device _selectedDevice;
        private string _response;
        private RelayCommand _refreshDevicesCommand;
        private RelayCommand _connectCommand;
        private RelayCommand _disconnectCommand;
        private RelayCommand _sendCommand;

        /// <summary>  
        /// Initializes a new instance of the <see cref="SenderViewModel"/> class.  
        /// </summary>  
        /// <param name="senderBluetoothService">  
        /// The Sender bluetooth service.  
        /// </param>  
        public CommandViewModel(ICommandResponseBluetoothService senderBluetoothService)
        {
            _service = senderBluetoothService;
        }

        /// <summary>  
        /// Gets or sets the devices.  
        /// </summary>  
        /// <value>  
        /// The devices.  
        /// </value>  
        public ObservableCollection<Device> Devices { get; } = new ObservableCollection<Device>();

        /// <summary>  
        /// Gets or sets the select device.  
        /// </summary>  
        /// <value>  
        /// The select device.  
        /// </value>  
        public Device SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                if (Set(ref _selectedDevice, value))
                {
                    RefreshCommands();
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
            set => Set( ref _data, value);
        }

        /// <summary>  
        /// Gets or sets the result value.  
        /// </summary>  
        /// <value>  
        /// The result value.  
        /// </value>  
        public string Response
        {
            get => _response;
            set => Set(ref _response, value);
        }

        private void RefreshCommands()
        {
            ConnectCommand.RaiseCanExecuteChanged();
            DisconnectCommand.RaiseCanExecuteChanged();
            SendCommand.RaiseCanExecuteChanged();
        }

        /// <summary>  
        /// Gets the send command.  
        /// </summary>  
        /// <value>  
        /// The send command.  
        /// </value>  
        public RelayCommand ConnectCommand
        {
            get
            {
                return _connectCommand
                       ?? (_connectCommand = new RelayCommand(
                           () =>
                           {
                               _service.Connect(this.SelectedDevice, Constants.ServiceClassId);
                               RefreshCommands();
                           },
                           () => !_service.IsConnected && SelectedDevice?.DeviceInfo != null
                       ));
            }
        }

        /// <summary>  
        /// Gets the send command.  
        /// </summary>  
        /// <value>  
        /// The send command.  
        /// </value>  
        public RelayCommand DisconnectCommand
        {
            get
            {
                return _disconnectCommand
                       ?? (_disconnectCommand = new RelayCommand(
                           () =>
                           {
                               _service.Disconnect();
                               RefreshCommands();
                           },
                           () => _service.IsConnected
                       ));
            }
        }

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
                           () => _service.IsConnected
                                 && !string.IsNullOrEmpty(Data)
                ));
            }
        }

        /// <summary>  
        /// Gets the send command.  
        /// </summary>  
        /// <value>  
        /// The send command.  
        /// </value>  
        public RelayCommand RefreshDevicesCommand
        {
            get
            {
                return _refreshDevicesCommand
                       ?? (_refreshDevicesCommand = new RelayCommand(
                           async()=>
                           {
                               Devices.Clear();
                               Devices.Add(new Device(null) { DeviceName = "Searching..." });
                               await RefreshDevices();
                           },
                           () => !_service.IsConnected
                       ));
            }
        }

        private async void SendData()
        {
            Response = await _service.Send(Data);
        }

        /// <summary>  
        /// Shows the device.  
        /// </summary>
        private async Task RefreshDevices()
        {
            var items = await _service.GetDevices();
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                SelectedDevice = null;
                Devices.Clear();
                foreach (var item in items)
                {
                    Devices.Add(item);
                }
            });
        }
    }
}

