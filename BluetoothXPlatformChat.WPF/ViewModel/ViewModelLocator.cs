/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:BluetoothXPlatformChat.WPF"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using System.Windows.Input;
using BluetoothXPlatformChat.Common.Interfaces;
using BluetoothXPlatformChat.WPF.Services;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace BluetoothXPlatformChat.WPF.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>  
        /// Initializes a new instance of the ViewModelLocator class.  
        /// </summary>  
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IReceiverBluetoothService, ReceiverBluetoothService>();
            SimpleIoc.Default.Register<ISenderBluetoothService, SenderBluetoothService>();
            SimpleIoc.Default.Register<ICommandResponseBluetoothService, CommandResponseBluetoothService>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CommandResponseViewModel>();
            SimpleIoc.Default.Register<ReceiverViewModel>();
            SimpleIoc.Default.Register<SenderViewModel>();
            SimpleIoc.Default.Register<CommandViewModel>();
            SimpleIoc.Default.Register<ResponderViewModel>();
        }

        /// <summary>  
        /// Gets the main.  
        /// </summary>  
        /// <value>The main.</value>  
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();


        /// <summary>  
        /// Gets the CommandResponseViewModel.  
        /// </summary>  
        /// <value>The main.</value>  
        public CommandResponseViewModel CommandResponseViewModel => ServiceLocator.Current.GetInstance<CommandResponseViewModel>();

        /// <summary>  
        /// Gets the Receiver view model.  
        /// </summary>  
        /// <value>The Receiver view model.</value>  
        public ReceiverViewModel ReceiverViewModel => ServiceLocator.Current.GetInstance<ReceiverViewModel>();

        /// <summary>  
        /// Gets the Sender view model.  
        /// </summary>  
        /// <value>The Sender view model.</value>  
        public SenderViewModel SenderViewModel => ServiceLocator.Current.GetInstance<SenderViewModel>();

        /// <summary>  
        /// Gets the Command view model.  
        /// </summary>  
        /// <value>The Command view model.</value>  
        public CommandViewModel CommandViewModel => ServiceLocator.Current.GetInstance<CommandViewModel>();

        /// <summary>  
        /// Gets the Command view model.  
        /// </summary>  
        /// <value>The Command view model.</value>  
        public ResponderViewModel ResponderViewModel => ServiceLocator.Current.GetInstance<ResponderViewModel>();

        /// <summary>  
        /// Cleanups this instance.  
        /// </summary>  
        public static void Cleanup()
        {
            foreach (IReceiverBluetoothService disposable in ServiceLocator.Current.GetAllInstances<IReceiverBluetoothService>())
            {
                disposable.Dispose();
            }
            foreach (ICommandResponseBluetoothService disposable in ServiceLocator.Current.GetAllInstances<ICommandResponseBluetoothService>())
            {
                disposable.Dispose();
            }
        }
    }
}