using System;
using Windows.Networking.Proximity;

namespace com.menvia.farol
{
    public class Beacon
    {
        public string UUID { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }

        //assinatura antiga private async bool ValidatePowerOn()
        public static bool ValidatePowerOn()
        {
            bool ret = false;
            // Search for all paired devices
            PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
            try
            {
                //var peers = await PeerFinder.FindAllPeersAsync();
                var peers = PeerFinder.FindAllPeersAsync();
                return true;                
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x8007048F)                
                    return false;                
            }
            return ret;
        }

        public static bool ValidateBLESupported()
        {
            bool ret = false;
           

            return ret;
        }
    }
}
