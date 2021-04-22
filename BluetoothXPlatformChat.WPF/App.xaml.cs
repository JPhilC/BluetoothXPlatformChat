using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BluetoothXPlatformChat.WPF.ViewModel;

namespace BluetoothXPlatformChat.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            // Call the ViewModelLocator cleanup
            ViewModelLocator.Cleanup();

            base.OnExit(e);
        }
    }

    
}
