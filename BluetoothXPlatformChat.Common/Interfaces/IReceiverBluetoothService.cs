using System;
using System.ComponentModel;

namespace BluetoothXPlatformChat.Common.Interfaces
{
    public interface IReceiverBluetoothService : INotifyPropertyChanged, IDisposable
    {
        bool WasStarted { get; set; }
        void Start(Action<string> reportAction);

        void Stop();
    }
}
