using BluetoothXPlatformChat.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BluetoothXPlatformChat.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}