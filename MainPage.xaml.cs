using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Farol_Beacon.Resources;
using Windows.Networking.Proximity;
using System.Windows.Media;
using Windows.Devices;
using Microsoft.Devices;
using Windows.Devices.Bluetooth;

namespace Farol_Beacon
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
                txtBluetooth.Text = "enabled";
                txtBluetooth.Foreground = new SolidColorBrush(Colors.Green);
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x8007048F)
                {
                    txtBluetooth.Text = "disabled";
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
                    txtBLE.Text = "not supported";
                    txtBLE.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    txtBLE.Text = "supported";
                    txtBLE.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
            catch
            {
                txtBLE.Text = "supported";
                txtBLE.Foreground = new SolidColorBrush(Colors.Green);
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