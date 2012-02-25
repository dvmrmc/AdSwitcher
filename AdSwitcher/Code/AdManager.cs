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
using Newtonsoft.Json;
using System.Threading;
using System.Diagnostics;
using AdSwitcher.Code.AdProvider;
using System.Collections.Generic;

namespace AdSwitcher.Code
{
    public class AdManager
    {
        private List<IAdProvider> _providers = null;
        private Timer _timer;

        private bool _started = false;
        private bool _settingsLoaded = false;

        private AdSwitcherSettings _settings;
        private AdSwitcherSettings Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                if (_settings != null)
                    _settingsLoaded = true;

                Start_Check();
            }
        }
        
        #region Singleton

        private static AdManager _instance;
        public static AdManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AdManager();

                return _instance;
            }
        }

        private AdManager()
        {
            _providers = new List<IAdProvider>();
        }

        #endregion

        public void Initialize()
        {
            //TODO: Init variables needed
        }

        public void LoadSettings(string description)
        {
            Debug.WriteLine("AdManager -> LoadSettings");
            Settings = JsonConvert.DeserializeObject<AdSwitcherSettings>(description);

            //CLEAN PROVIDERS
            _providers.Clear();

            foreach (AdProviderSettings providerSettings in Settings.Providers)
            {
                IAdProvider provider = AdProviderFactory.Instance.CreateProvider(providerSettings);

                if (provider != null)
                    _providers.Add(provider);
                else
                    Debug.WriteLine("AdManager -> LoadSettings -> description of " + providerSettings.Name + " was corrupted, please check description on JSON");
            }
        }

        public void Start()
        {
            Debug.WriteLine("AdManager -> Start");
            _started = true;
            Start_Check();
        }

        private void Start_Check()
        {
            Debug.WriteLine("AdManager -> StartCheck with started: " + _started + " settingsLoaded: " + _settingsLoaded);

            if (_started && _settingsLoaded)
                _timer = new Timer(Refresh_Tick, null, 0, -1);
        }

        private void Refresh_Tick(object data)
        {
            Debug.WriteLine("======================================================");
            Debug.WriteLine("AdManager -> Refresh Tick");
        }
    }
}
