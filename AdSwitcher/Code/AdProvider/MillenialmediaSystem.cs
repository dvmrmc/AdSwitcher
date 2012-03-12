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
using mmiWP7SDK;
using System.Diagnostics;

namespace AdSwitcher.Code.AdProvider
{
    public class MillenialmediaSystem : AdProvider
    {
        public string AppID = "";

        public MillenialmediaSystem()
        {
            _name = Constants.PROVIDER_MILLENIALMEDIA;
        }

        public override UIElement CreateControl()
        {
            base.CreateControl();

            _control = new MMAdView()
            {
                Name = "millenialmediaSystem",
                Width = this.Width,
                Height = this.Height,
                Apid = this.AppID,
                RefreshTimer = -1,
                AdType = MMAdView.MMAdType.MMBannerAdBottom
            };

            (_control as MMAdView).MMAdSuccess += MillenialmediaSystem_MMAdSuccess;
            (_control as MMAdView).MMAdFailure += MillenialmediaSystem_MMAdFailure;

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

            (_control as MMAdView).MMAdSuccess -= MillenialmediaSystem_MMAdSuccess;
            (_control as MMAdView).MMAdFailure -= MillenialmediaSystem_MMAdFailure;

            _control = null;
        }

        public override void Start()
        {
            base.Start();

            if (_control != null)
                (_control as MMAdView).CallForAd();
        }

        public override void Refresh()
        {
            base.Refresh();

            if (_control != null)
                (_control as MMAdView).CallForAd();
        }

        #region Events

        private void MillenialmediaSystem_MMAdSuccess(object sender, EventArgs e)
        {
            Debug.WriteLine("AdProvider: " + _name + " -> Event New");
            RaiseNew();
        }

        private void MillenialmediaSystem_MMAdFailure(object sender, EventArgs e)
        {
            Debug.WriteLine("AdProvider: " + _name + " -> Event Error");
            RaiseError();
        }

        #endregion
    }
}
