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
using AdDuplex;
using System.Diagnostics;

namespace AdSwitcher.Code.AdProvider
{
    public class AdduplexSystem : AdProvider
    {
        public string AppID = "";

        public AdduplexSystem()
        {
            _name = Constants.PROVIDER_ADDUPLEX;
        }

        public override UIElement CreateControl()
        {
            base.CreateControl();

            _control = new AdControl()
            {
                Name = "adduplexSytem",
                Width = this.Width,
                Height = this.Height,
                AppId = this.AppID,
                RefreshInterval = 20
            };

            (_control as AdControl).AdLoaded += AdduplexSystem_AdLoaded;
            (_control as AdControl).AdLoadingError += AdduplexSystem_AdLoadingError;

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

            (_control as AdControl).AdLoaded -= AdduplexSystem_AdLoaded;
            (_control as AdControl).AdLoadingError -= AdduplexSystem_AdLoadingError;

            _control = null;
        }

        //TODO: Do something to Start & Refresh, by now, it's not supported and the control will be destroyed, removed and added to start and refresh

        #region Events

        void AdduplexSystem_AdLoadingError(object sender, AdLoadingErrorEventArgs e)
        {
            Debug.WriteLine("AdProvider: " + _name + " -> Event Error");
            Debug.WriteLine("Adduplex Error: " + e.Error);

            RaiseError();
        }

        void AdduplexSystem_AdLoaded(object sender, AdLoadedEventArgs e)
        {
            Debug.WriteLine("AdProvider: " + _name + " -> Event New");
            
            RaiseNew();
        }

        #endregion
    }
}
