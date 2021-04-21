using InTheHand.Net.Sockets;

namespace BluetoothXPlatformChat.Common.Model
{
    public sealed class Device
    {
        /// <summary>  
        /// Gets or sets the device name.  
        /// </summary>  
        /// <value>  
        /// The device name.  
        /// </value>  
        public string DeviceName { get; set; }

        /// <summary>  
        /// Gets or sets a value indicating whether authenticated.  
        /// </summary>  
        /// <value>  
        /// The authenticated.  
        /// </value>  
        public bool IsAuthenticated { get; set; }

        /// <summary>  
        /// Gets or sets a value indicating whether is connected.  
        /// </summary>  
        /// <value>  
        /// The is connected.  
        /// </value>  
        public bool IsConnected { get; set; }

        /// <summary>  
        /// Gets or sets a value indicating whether remembered.  
        /// </summary>  
        /// <value>  
        /// The remembered.  
        /// </value>  
        public bool Remembered { get; set; }

        /// <summary>  
        /// Gets or sets the device info.  
        /// </summary>  
        /// <value>  
        /// The device info.  
        /// </value>  
        public BluetoothDeviceInfo DeviceInfo { get; set; }

        /// <summary>  
        /// Initializes a new instance of the <see cref="Device"/> class.  
        /// </summary>  
        /// <param name="deviceInfo">  
        /// The device_info.  
        /// </param>  
        public Device(BluetoothDeviceInfo deviceInfo)
        {
            if (deviceInfo != null)
            {
                DeviceInfo = deviceInfo;
                IsAuthenticated = deviceInfo.Authenticated;
                IsConnected = deviceInfo.Connected;
                DeviceName = deviceInfo.DeviceName;
            }
        }

        /// <summary>  
        /// The to string.  
        /// </summary>  
        /// <returns>  
        /// The <see cref="string"/>.  
        /// </returns>  
        public override string ToString()
        {
            return DeviceName;
        }
    }
}
