using System.Runtime.CompilerServices;
using System.Windows;
using BluetoothXPlatformChat.Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace BluetoothXPlatformChat.WPF.ViewModel
{
    public sealed class MainViewModel : ViewModelBase
    {
        private bool _isReceiver = true;
        private bool _isSender;

        /// <summary>  
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.  
        /// </summary>  
        public MainViewModel()
        {
        }

        /// <summary>  
        /// Gets or sets a value indicating whether is Receiver.  
        /// </summary>  
        /// <value>  
        /// The is Receiver.  
        /// </value>  
        public bool IsReceiver
        {
            get
            {
                return _isReceiver;
            }
            set
            {
                if (Set(ref _isReceiver, value))
                {
                    _isSender = !_isReceiver;
                    RaisePropertyChanged("IsSender");
                    BroadcastChange();
                }
            }
        }

        /// <summary>  
        /// Gets or sets a value indicating whether is Sender.  
        /// </summary>  
        /// <value>  
        /// The is Sender.  
        /// </value>  
        public bool IsSender
        {
            get
            {
                return _isSender;
            }
            set
            {
                if (Set(ref _isSender, value))
                {
                    _isReceiver = !_isSender;
                    RaisePropertyChanged("IsReceiver");
                    BroadcastChange();
                }
            }
        }

        private void BroadcastChange()
        {
            Messenger.Default.Send(IsSender ? new Message(true) : new Message(false));
        }
        
    }
}