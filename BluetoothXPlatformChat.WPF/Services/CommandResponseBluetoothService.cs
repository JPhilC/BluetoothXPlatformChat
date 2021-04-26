using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BluetoothXPlatformChat.Common;
using BluetoothXPlatformChat.Common.Interfaces;
using BluetoothXPlatformChat.Common.Model;
using InTheHand.Net.Sockets;

namespace BluetoothXPlatformChat.WPF.Services
{
    public class CommandResponseBluetoothService : ICommandResponseBluetoothService
    {
        public event EventHandler<BluetoothCommandResponseEventArgs> CommandReceived;

        private BluetoothClient _client;
        private BluetoothListener _listener;

        private CancellationTokenSource _cancelToken;
        private bool _isListening;
        private bool _isConnected;

        public bool IsConnected => _isConnected;
        public bool IsListening => _isListening;

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


        public bool Connect(Device device, Guid serviceClassId)
        {
            if (_isListening)
            {
                throw new BluetoothServiceException("Service cannot connect while already listening");
            }

            if (_isConnected)
            {
                throw new BluetoothServiceException("Service is already connected.");
            }
            _isConnected = true;
            _client = new BluetoothClient();
            _client.Connect(device.DeviceInfo.DeviceAddress, serviceClassId);
            if (_client.Connected)
            {
                //_stream = _client.GetStream();
                //_streamReader = new StreamReader(_stream);
            }
            else
            {
                _client.Close();
                _client.Dispose();
                _client = null;
                _isConnected = false;
            }
            return _isConnected;
        }

        public void Disconnect()
        {
            //_streamReader.Close();
            //_stream.Close();
            _client.Close();
            //_streamReader.Dispose();
            //_stream.Close();
            _client.Dispose();
            _client = null;
            _isConnected = false;
        }

        public async Task<string> Send(string command)
        {
            string response = "ERR:TIMEOUT";
            using (var stream = _client.GetStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                StreamReader reader = new StreamReader(stream);
                //var buffer = Encoding.UTF8.GetBytes(command);
                //await stream.WriteAsync(buffer, 0, buffer.Length);
                //await stream.FlushAsync();
                //stream.Close();
                await writer.WriteAsync(command);
                await writer.FlushAsync();
                //writer.Close();
                while (stream.DataAvailable)
                {
                    response += await reader.ReadToEndAsync();
                }
            }
            return response;
        }


        public void StartListening(Guid serviceClassId)
        {
            if (_isConnected)
            {
                throw new BluetoothServiceException("Service cannot start listing when it is a sender.");
            }

            if (_isListening)
            {
                throw new BluetoothServiceException("Service is already listening.");
            }

            _isListening = true;
            if (_cancelToken != null && _listener != null)
            {
                Dispose(true);
            }
            _listener = new BluetoothListener(serviceClassId)
            {
                ServiceName = "MyService"
            };

            _listener.Start();
            _cancelToken = new CancellationTokenSource();

            Task.Run(() => Listener(_cancelToken));
        }



        public void StopListening()
        {
            if (_isConnected)
            {
                throw new BluetoothServiceException("Service cannot stop listing when it is a sender.");
            }
            // If the service isn't listening just return
            if (!_isListening) return;

            _cancelToken.Cancel();
            _listener.Dispose();
            _listener = null;
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
            }
            _isListening = false;

        }

        /// <summary>  
        /// Listeners the accept bluetooth client.  
        /// </summary>  
        /// <param name="token">  
        /// The token.  
        /// </param>  
        private async Task Listener(CancellationTokenSource token)
        {
            try
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    // Are we still listening for a client
                    if (_client == null)
                    {
                        _client = _listener.AcceptBluetoothClient();
                    }
                    // If we have a client see if there is new data
                    if (_client != null)
                    {
                        using (var stream = _client.GetStream())
                        using (var reader = new StreamReader(stream))
                        using (var writer = new StreamWriter(stream))
                        {
                            try
                            {
                                var command = await reader.ReadToEndAsync();
                                if (!string.IsNullOrEmpty(command))
                                {
                                    BluetoothCommandResponseEventArgs args = new BluetoothCommandResponseEventArgs()
                                    {
                                        Command = command
                                    };
                                    OnCommandReceived(args);
                                    // Now post the response
                                    await writer.WriteAsync(args.Response);
                                    await writer.FlushAsync();
                                }
                            }
                            catch (IOException)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // todo handle the exception  
                // for the sample it will be ignored
                Debug.WriteLine($"Exception thrown: {ex.Message}");
            }
        }

        void OnCommandReceived(BluetoothCommandResponseEventArgs args)
        {
            EventHandler<BluetoothCommandResponseEventArgs> ev = CommandReceived;
            ev?.Invoke(this, args);
        }

        #region IDisposable ...
        private bool _disposedValue;


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_listener != null)
                    {
                        _listener.Dispose();
                        _listener = null;
                    }
                    if (_client != null)
                    {
                        _client.Dispose();
                        _client = null;
                    }
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
