using BluetoothXPlatformChat.Common.Interfaces;
using BluetoothXPlatformChat.Common.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace BluetoothXPlatformChat.ViewModels
{
    public class ReceiveViewModel : BaseViewModel
    {
        private readonly IReceiverBluetoothService _receiverBluetoothService;
        private string _data;
        private string _status;

        public ReceiveViewModel(IReceiverBluetoothService receiverBluetoothService)
        {
            Title = "Receive";
            _receiverBluetoothService = receiverBluetoothService;
            _receiverBluetoothService.PropertyChanged += ReceiverBluetoothService_PropertyChanged;
            Data = "N/D";
            Status = "N/D";

            Messenger.Default.Register<Message>(this, ResetAll);
        }

        /// <summary>  
        /// Resets all.  
        /// </summary>  
        /// <param name="message">The message.</param>  
        private void ResetAll(Message message)
        {
            if (!message.IsToShowDevices)
            {
                if (_receiverBluetoothService.WasStarted)
                {
                    _receiverBluetoothService.Stop();
                }
                IsListening = false;
                Data = "N/D";
                Status = "N/D";
            }
        }

        /// <summary>  
        /// The set data received.  
        /// </summary>  
        /// <param name="data">  
        /// The data.  
        /// </param>  
        public void SetData(string data)
        {
            Data = data;
        }

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
                           _receiverBluetoothService.Start(SetData);
                           Data = "Can receive data.";
                       }, () => !IsListening));
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
                        _receiverBluetoothService.Stop();
                        Data = "Cannot receive data.";
                    }, () => IsListening));
            }
        }

        private bool _isListening;
        /// <summary>  
        /// Gets or sets a value indicating whether the app is listening.  
        /// </summary>  
        /// <value>  
        /// The is star enabled.  
        /// </value>  
        public bool IsListening
        {
            get => _isListening;
            set
            {
                Set(ref _isListening, value);
                StartCommand.RaiseCanExecuteChanged();
                StopCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>  
        /// Gets or sets the status.  
        /// </summary>  
        /// <value>The status.</value>  
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        /// <summary>  
        /// Handles the PropertyChanged event of the ReceiverBluetoothService control.  
        /// </summary>  
        /// <param name="sender">The source of the event.</param>  
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>  
        private void ReceiverBluetoothService_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WasStarted")
            {
                IsListening = _receiverBluetoothService.WasStarted;
            }
        }
    }
}