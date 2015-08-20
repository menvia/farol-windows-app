using com.menvia.farol.Resources;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;

namespace com.menvia.farol
{    
    public partial class BeaconList : PhoneApplicationPage
    {        
        private string UUID = "64657665-6c6f-7064-6279-6d656e766961";        
        public BeaconList()
        {
            InitializeComponent();            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BuscarBLE();
            
            base.OnNavigatedTo(e);
        }

		private async void BuscarBLE()
		{
			Beacon objBeacon;
			List<Beacon> objListBeacon;
			var selector = BluetoothLEDevice.GetDeviceSelector();			
			objListBeacon = new List<Beacon>();


			foreach (DeviceInformation di in await DeviceInformation.FindAllAsync(selector))
			{
				BluetoothDevice bleDevice = await BluetoothDevice.FromIdAsync(di.Id);
				if (bleDevice.DeviceId.Equals(UUID))
				{
					objBeacon = new Beacon();
					objBeacon.UUID = bleDevice.DeviceId;
					objBeacon.Major = Convert.ToInt32(bleDevice.ClassOfDevice.MajorClass.ToString());
					objBeacon.Minor = Convert.ToInt32(bleDevice.ClassOfDevice.MinorClass.ToString());
					objListBeacon.Add(objBeacon);
				}
			}
			if (objListBeacon != null && objListBeacon.Count > 0)
				listBoxBeacons.ItemsSource = objListBeacon;
			else
				MessageBox.Show(AppResources.msgNoDevicesFound);
				//MessageBox.Show("No devices found.");
		}

        private async void BuscarBLE2()
        {
            Beacon objBeacon;
            List<Beacon> objListBeacon;            
            var devices = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(GattDeviceService.GetDeviceSelectorFromUuid(GattServiceUuids.GenericAccess));
            //variável devices, no Lumia 720 sempre está sem nenhum dispositivo.
            if (devices.Count == 0)
            {
                //MessageBox.Show("No device found.");				
				MessageBox.Show(AppResources.msgNoDevicesFound);
                return;
            }
			else
			{
				//MessageBox.Show(devices.Count + " device(s) found.");
				MessageBox.Show(devices.Count + AppResources.msgDevicesFound);
			}

            objListBeacon = new List<Beacon>();
			

            foreach(DeviceInformation di in devices){
                var service = await GattDeviceService.FromIdAsync(di.Id);
                if (service == null)
                    break;
                var characteristic = service.GetCharacteristics(GattCharacteristic.ConvertShortIdToUuid(0x2A00))[0];			
                objBeacon = new Beacon();
                objBeacon.Major = 0;
                objBeacon.Minor = 0;
                objBeacon.UUID = characteristic.Uuid.ToString();
                objListBeacon.Add(objBeacon);
            }
            if (objListBeacon != null && objListBeacon.Count > 0)
                listBoxBeacons.ItemsSource = objListBeacon;
        }        
    }
}