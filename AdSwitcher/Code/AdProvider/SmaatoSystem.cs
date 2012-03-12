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
using SOMAWP7;
using System.Diagnostics;

namespace AdSwitcher.Code.AdProvider
{
    public class SmaatoSystem : AdProvider
    {
        public int AdSpaceID = 0;
        public int PublisherID = 0;

        public SmaatoSystem()
        {
            _name = Constants.PROVIDER_SMAATO;
        }

        public override UIElement CreateControl()
        {
            base.CreateControl();

            _control = new SomaAdViewer()
            {
                Name = "pubcenterSystem",
                Width = this.Width,
                Height = this.Height,
                Adspace = this.AdSpaceID,
                Pub = this.PublisherID,
                PopupAd = false,
                AdInterval = -1
            };

            (_control as SomaAdViewer).AdError += SmaatoSystem_AdError;
            (_control as SomaAdViewer).NewAdAvailable += SmaatoSystem_NewAdAvailable;

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

            (_control as SomaAdViewer).AdError -= SmaatoSystem_AdError;
            (_control as SomaAdViewer).NewAdAvailable -= SmaatoSystem_NewAdAvailable;

            _control = null;
        }

        public override void Start()
        {
            base.Start();

            (_control as SomaAdViewer).StartAds();
        }

        public override void Refresh()
        {
            base.Refresh();

            (_control as SomaAdViewer).StartAds();
        }

        #region Events

        void SmaatoSystem_NewAdAvailable(object sender, EventArgs e)
        {
            (_control as SomaAdViewer).StopAds();

            Debug.WriteLine("AdProvider: " + _name + " -> Event New");
            RaiseNew();
        }

        void SmaatoSystem_AdError(object sender, string ErrorCode, string ErrorDescription)
        {
            (_control as SomaAdViewer).StopAds();

            Debug.WriteLine("AdProvider: " + _name + " -> Event Error");
            Debug.WriteLine("Smaato Error: " + ErrorCode + " - Description: " + ErrorDescription);

            RaiseError();
        }

        #endregion
    }
}
