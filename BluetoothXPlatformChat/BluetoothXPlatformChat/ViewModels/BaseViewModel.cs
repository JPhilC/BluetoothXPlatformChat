using GalaSoft.MvvmLight;

namespace BluetoothXPlatformChat.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {

        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}
