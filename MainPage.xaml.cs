using com.menvia.farol.Resources;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using Windows.Devices.Bluetooth;
using Windows.Networking.Proximity;

namespace com.menvia.farol
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ValidatePowerOn();
            ValidateBLESupported();
            base.OnNavigatedTo(e);
        }

        private async void ValidatePowerOn()
        {
            // Search for all paired devices
            PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
            try
            {
                var peers = await PeerFinder.FindAllPeersAsync();
                //txtBluetooth.Text = "enabled";
				txtBluetooth.Text = AppResources.txtBluetoothEnabled;
                txtBluetooth.Foreground = new SolidColorBrush(Colors.Green);
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x8007048F)
                {
                    //txtBluetooth.Text = "disabled";
					txtBluetooth.Text = AppResources.txtBluetoothDisabled;
                    txtBluetooth.Foreground = new SolidColorBrush(Colors.Red);
                }
            }            
        }       

        private void ValidateBLESupported()
        {
            try
            {
                string ble = BluetoothLEDevice.GetDeviceSelector();
                if (string.IsNullOrEmpty(ble))
                {
                    //txtBLE.Text = "not supported";
					txtBLE.Text = AppResources.txtBLENotSupported;
                    txtBLE.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    //txtBLE.Text = "supported";
					txtBLE.Text = AppResources.txtBLESupported;
                    txtBLE.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
            catch
            {
                //txtBLE.Text = "not supported";
				txtBLE.Text = AppResources.txtBLENotSupported;
                txtBLE.Foreground = new SolidColorBrush(Colors.Red);
            }            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
            NavigationService.Navigate(new Uri("/BeaconList.xaml", UriKind.Relative));
        }        

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}