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
using Google.AdMob.Ads.WindowsPhone7.WPF;
using Google.AdMob.Ads.WindowsPhone7;
using System.Diagnostics;

namespace AdSwitcher.Code.AdProvider
{
    public class AdmobSystem : AdProvider
    {
        public string AdUnitID = "";

        public AdmobSystem()
        {
            _name = Constants.PROVIDER_ADMOB;
        }

        public override UIElement CreateControl()
        {
            base.CreateControl();

            _control = new BannerAd()
            {
                Name = "admobSystem",
                Width = this.Width,
                Height = this.Height,
                AdUnitID = this.AdUnitID                
            };

            (_control as BannerAd).AdReceived += AdmobSystem_AdReceived;
            (_control as BannerAd).AdFailed += AdmobSystem_AdFailed;

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

            (_control as BannerAd).AdReceived -= AdmobSystem_AdReceived;
            (_control as BannerAd).AdFailed -= AdmobSystem_AdFailed;

            _control = null;
        }

        public override void Refresh()
        {
            base.Refresh();

            if(_control != null)
                (_control as BannerAd).ShowNewAd();
        }

        #region Events

        void AdmobSystem_AdReceived(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("AdProvider: " + _name + " -> Event New");
            RaiseNew();
        }

        void AdmobSystem_AdFailed(object sender, AdException exception)
        {
            Debug.WriteLine("AdProvider: " + _name + " -> Event Error");
            Debug.WriteLine("AdProvider error: " + exception);

            RaiseError();
        }

        #endregion

    }
}
