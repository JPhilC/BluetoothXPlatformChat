using System.Runtime.CompilerServices;
using System.Windows;
using BluetoothXPlatformChat.Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace BluetoothXPlatformChat.WPF.ViewModel
{
    public sealed class CommandResponseViewModel : ViewModelBase
    {
        private bool _isResponder = true;
        private bool _isCommander;

        /// <summary>  
        /// Initializes a new instance of the <see cref="CommandResponseViewModel"/> class.  
        /// </summary>  
        public CommandResponseViewModel()
        {
        }

        /// <summary>  
        /// Gets or sets a value indicating whether is Receiver.  
        /// </summary>  
        /// <value>  
        /// The is Receiver.  
        /// </value>  
        public bool IsResponder
        {
            get
            {
                return _isResponder;
            }
            set
            {
                if (Set(ref _isResponder, value))
                {
                    _isCommander = !_isResponder;
                    RaisePropertyChanged("IsCommander");
                }
            }
        }

        /// <summary>  
        /// Gets or sets a value indicating whether is Sender.  
        /// </summary>  
        /// <value>  
        /// The is Sender.  
        /// </value>  
        public bool IsCommander
        {
            get
            {
                return _isCommander;
            }
            set
            {
                if (Set(ref _isCommander, value))
                {
                    _isResponder = !_isCommander;
                    RaisePropertyChanged("IsResponder");
                }
            }
        }
    }
}