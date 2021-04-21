namespace BluetoothXPlatformChat.Common.Model
{
    public class Message
    {
        public bool IsToShowDevices { get; set; }

        public Message(bool isToShowDevices)
        {
            this.IsToShowDevices = isToShowDevices;
        }


    }
}
