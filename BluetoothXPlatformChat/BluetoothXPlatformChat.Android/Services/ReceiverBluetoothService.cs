using System;
using System.ComponentModel;
using BluetoothXPlatformChat.Common.Interfaces;
using GalaSoft.MvvmLight;

namespace BluetoothXPlatformChat.Droid.Services
{
    public class ReceiverBluetoothService: ObservableObject, IDisposable, IReceiverBluetoothService
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool WasStarted { get; set; }
        public void Start(Action<string> reportAction)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}