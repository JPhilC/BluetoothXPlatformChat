using System;
using System.Collections.Generic;
using System.Linq;
using BluetoothXPlatformChat.Common.Interfaces;
using System.Threading.Tasks;
using BluetoothXPlatformChat.Common;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using Device = BluetoothXPlatformChat.Common.Model.Device;

namespace BluetoothXPlatformChat.Droid.Services
{
    public class SenderBluetoothService : ISenderBluetoothService
    {

        public async Task<IList<Device>> GetDevices()
        {
            BluetoothRadio radio = BluetoothRadio.Default;
            if (radio != null && radio.Mode == RadioMode.PowerOff)
            {
                BluetoothRadio.Default.Mode = RadioMode.Connectable;
            }

            // for not block the UI it will run in a different threat  
            var task = Task.Run(() =>
            {
                var devices = new List<Device>();
                using (var bluetoothClient = new BluetoothClient())
                {
                    var array = bluetoothClient.DiscoverDevices(21).ToArray();
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
                try
                {
                    using (BluetoothClient bluetoothClient = new BluetoothClient())
                    {
                        // connecting  
                        bluetoothClient.Connect(device.DeviceInfo.DeviceAddress, Constants.ServiceClassId);


                        using (var bluetoothStream = bluetoothClient.GetStream())
                        {

                            // get stream for send the data  

                            // if all is ok to send  
                            if (bluetoothClient.Connected && bluetoothStream != null)
                            {
                                // write the data in the stream  
                                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                                bluetoothStream.Write(buffer, 0, buffer.Length);
                                bluetoothStream.FlushAsync();
                                bluetoothStream.Close();

                                return true;
                            }
                        }

                        return false;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    // the error will be ignored and the send data will report as not sent  
                    // for understood the type of the error, handle the exception  
                }

                return false;
            });
            return await task;
        }

    }
}