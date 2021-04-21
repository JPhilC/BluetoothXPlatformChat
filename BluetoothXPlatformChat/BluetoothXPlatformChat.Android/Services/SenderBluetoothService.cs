using System;
using System.Collections.Generic;
using BluetoothXPlatformChat.Common.Interfaces;
using System.Threading.Tasks;
using Device = BluetoothXPlatformChat.Common.Model.Device;

namespace BluetoothXPlatformChat.Droid.Services
{
    public class SenderBluetoothService : ISenderBluetoothService
    {
        public Task<IList<Device>> GetDevices()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Send(Device device, string content)
        {
            throw new NotImplementedException();
        }
    }
}