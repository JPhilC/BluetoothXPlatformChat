using System;
using System.ComponentModel;

namespace BluetoothXPlatformChat.Common.Interfaces
{
    public interface IReceiverBluetoothService : INotifyPropertyChanged
    {
        bool WasStarted { get; set; }
        void Start(Action<string> reportAction);

        void Stop();
    }
}
