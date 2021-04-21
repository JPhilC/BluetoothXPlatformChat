using BluetoothXPlatformChat.Common.Interfaces;
using BluetoothXPlatformChat.Common.Model;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BluetoothXPlatformChat.Common;

namespace BluetoothXPlatformChat.WPF.Services
{
    public sealed class SenderBluetoothService : ISenderBluetoothService
    {
        /// <summary>  
        /// Gets the devices.  
        /// </summary>  
        /// <returns>The list of the devices.</returns>  
        public async Task<IList<Device>> GetDevices()
        {
            // for not block the UI it will run in a different threat  
            var task = Task.Run(() =>
            {
                var devices = new List<Device>();
                using (var bluetoothClient = new BluetoothClient())
                {
                    var array = bluetoothClient.DiscoverDevices().ToArray();
                    var count = array.Length;
                    for (var i = 0; i < count; i++)
                    {
                        devices.Add(new Device(array[i]));
                    }
                }
                return devices;
            });
            return await task;
        }

        /// <summary>  
        /// Sends the data to the Receiver.  
        /// </summary>  
        /// <param name="device">The device.</param>  
        /// <param name="content">The content.</param>  
        /// <returns>If was sent or not.</returns>  
        public async Task<bool> Send(Device device, string content)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException("content");
            }

            // for not block the UI it will run in a different threat  
            var task = Task.Run(() =>
            {
                using (var bluetoothClient = new BluetoothClient())
                {
                    try
                    {
                        // connecting  
                        bluetoothClient.Connect(device.DeviceInfo.DeviceAddress, Constants.ServiceClassId);
                        // bluetoothClient.Connect(device.DeviceInfo.DeviceAddress, BluetoothService.SerialPort);

                        // get stream for send the data  
                        var bluetoothStream = bluetoothClient.GetStream();

                        // if all is ok to send  
                        if (bluetoothClient.Connected && bluetoothStream != null)
                        {
                            // write the data in the stream  
                            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                            bluetoothStream.Write(buffer, 0, buffer.Length);
                            bluetoothStream.Flush();
                            bluetoothStream.Close();
                            return true;
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        // the error will be ignored and the send data will report as not sent  
                        // for understood the type of the error, handle the exception  
                    }
                }
                return false;
            });
            return await task;
        }
    }
}
