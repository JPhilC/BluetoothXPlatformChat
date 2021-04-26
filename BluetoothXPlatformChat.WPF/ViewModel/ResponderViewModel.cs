using System.Collections.Generic;
using System.Windows.Input;
using BluetoothXPlatformChat.Common;
using BluetoothXPlatformChat.Common.Interfaces;
using BluetoothXPlatformChat.Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace BluetoothXPlatformChat.WPF.ViewModel
{
    public sealed class ResponderViewModel : ViewModelBase
    {
        private readonly ICommandResponseBluetoothService _service;
        private string _data;
        private bool _isStarEnabled;
        private string _status;

        private Dictionary<string, string> _commands = new Dictionary<string, string>()
        {
            {"ONE", "One received"},
            {"TWO", "CMD_OK"}
        };

        /// <summary>  
        /// Initializes a new instance of the <see cref="ReceiverViewModel" /> class.  
        /// </summary>  
        /// <param name="receiverBluetoothService">The Receiver bluetooth service.</param>  
        public ResponderViewModel(ICommandResponseBluetoothService service)
        {
            _service = service;
            _service.CommandReceived += service_CommandReceived;
            Status = "N/D";


        }


        private void service_CommandReceived(object sender, BluetoothCommandResponseEventArgs e)
        {
            string command = e.Command;
            Data = command;
            e.Response = _commands.TryGetValue(command, out var response) ? response : "ERR:COMMAND_UNKNOWN";
        }

        public Dictionary<string, string> Commands => _commands;

        /// <summary>  
        /// Gets or sets the data.  
        /// </summary>  
        /// <value>  
        /// The data received.  
        /// </value>  
        public string Data
        {
            get => _data;
            set => Set(ref _data, value);
        }

        private RelayCommand _startCommand;

        /// <summary>  
        /// Gets the start command.  
        /// </summary>  
        /// <value>  
        /// The start command.  
        /// </value>  
        public RelayCommand StartCommand
        {
            get
            {
                return _startCommand
                       ?? (_startCommand = new RelayCommand(() =>
                           {
                               _service.StartListening(Constants.ServiceClassId);
                               Status = "Can receive data.";
                               StopCommand.RaiseCanExecuteChanged();
                               StartCommand.RaiseCanExecuteChanged();
                           },
                               () => !_service.IsListening
                               )
                       );
            }
        }

        private RelayCommand _stopCommand;

        /// <summary>  
        /// Gets the stop command.  
        /// </summary>  
        /// <value>  
        /// The stop command.  
        /// </value>  
        public RelayCommand StopCommand
        {
            get
            {
                return _stopCommand
                       ?? (_stopCommand = new RelayCommand(() =>
                           {
                               _service.StopListening();
                               Status = "Cannot receive data.";
                               StartCommand.RaiseCanExecuteChanged();
                               StopCommand.RaiseCanExecuteChanged();
                           },
                               () => _service.IsListening
                               )
                       );
            }
        }


        /// <summary>  
        /// Gets or sets the status.  
        /// </summary>  
        /// <value>The status.</value>  
        public string Status
        {
            get { return _status; }
            set { Set(() => Status, ref _status, value); }
        }

    }
}
