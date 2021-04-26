using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothXPlatformChat.Common
{
    public class BluetoothServiceException: Exception
    {
        public BluetoothServiceException(string message) : base(message)
        {
        }
    }
}
