using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Advertising.Mobile.UI;
using Microsoft.Advertising;
using System.Diagnostics;

namespace AdSwitcher.Code.AdProvider
{
    public class PubcenterSystem : AdProvider
    {
        public string AdUnitID = "";
        public string ApplicationID = "";

        public PubcenterSystem()
        {
            _name = Constants.PROVIDER_PUBCENTER;
        }

        public override UIElement CreateControl()
        {
            base.CreateControl();

            _control = new AdControl()
            {
                Width = this.Width,
                Height = this.Height,
                Name = "pubcenterSystem",
                AdUnitId = this.AdUnitID,
                ApplicationId = this.ApplicationID,
                IsAutoRefreshEnabled = false
            };

            (_control as AdControl).AdRefreshed += PubcenterSystem_AdRefreshed;
            (_control as AdControl).ErrorOccurred += PubcenterSystem_ErrorOcurred;

            return _control;
        }

        public override void PurgeControl()
        {
            base.PurgeControl();

            if (_control == null)
            {
                Debug.WriteLine("AdProvider: " + _name + " -> PurgeControl -> Control was null, purge ineffective");
                return;
            }

            (_control as AdControl).AdRefreshed -= PubcenterSystem_AdRefreshed;
            (_control as AdControl).ErrorOccurred -= PubcenterSystem_ErrorOcurred;
            
            _control = null;
        }

        private void PubcenterSystem_AdRefreshed(object sender, EventArgs e)
        {
            Debug.WriteLine("AdProvider: " + _name + " -> Event New");
            RaiseNew();
        }

        private void PubcenterSystem_ErrorOcurred(object sender, AdErrorEventArgs e)
        {
            Debug.WriteLine("AdProvider: " + _name + " -> Event Error");
            Debug.WriteLine("Pubcenter Error: " + e.Error);

            RaiseError();
        }
    }
}
