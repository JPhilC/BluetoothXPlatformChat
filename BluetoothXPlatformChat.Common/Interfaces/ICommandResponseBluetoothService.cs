using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BluetoothXPlatformChat.Common.Model;

namespace BluetoothXPlatformChat.Common.Interfaces
{
    public class BluetoothCommandResponseEventArgs
    {
        public string Command { get; set; }
        public string Response { get; set; }
    }

    public interface ICommandResponseBluetoothService: IDisposable
    {
        bool IsConnected { get; }

        bool IsListening { get; }

        Task<IList<Device>> GetDevices();

        bool Connect(Device device, Guid serviceClassId);

        void Disconnect();

        Task<string> Send(string command);

        void StartListening(Guid serviceClassId);

        void StopListening();

        event EventHandler<BluetoothCommandResponseEventArgs> CommandReceived;
    }
}
