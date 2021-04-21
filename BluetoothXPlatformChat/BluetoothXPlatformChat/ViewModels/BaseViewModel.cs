using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BluetoothXPlatformChat.Models;
using BluetoothXPlatformChat.Services;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace BluetoothXPlatformChat.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

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
