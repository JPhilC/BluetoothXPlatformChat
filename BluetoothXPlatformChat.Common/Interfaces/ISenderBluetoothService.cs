using BluetoothXPlatformChat.Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BluetoothXPlatformChat.Common.Interfaces
{
    public interface ISenderBluetoothService
    {
        Task<IList<Device>> GetDevices();

        Task<bool> Send(Device device, string content);
    }
}
