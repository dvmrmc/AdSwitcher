using System;
using System.Linq;
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
        private List<IAdProvider> _availableProviders = null;
        
        private Timer _timer;
        private int _refreshSeconds;

        private Panel _rotatingBannerContainer;
        private Panel _defaultBannerContainer;

        private IAdProvider _newProvider;
        private IAdProvider _currentProvider;

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
            _availableProviders = new List<IAdProvider>();
        }

        #endregion

        public void Initialize(Panel rotatingAdContainer, Panel defaultAdContainer)
        {
            _rotatingBannerContainer = rotatingAdContainer;
            _defaultBannerContainer = defaultAdContainer;
            //TODO: Init variables needed
        }

        public void LoadSettings(string description)
        {
            Debug.WriteLine("AdManager -> LoadSettings");
            Settings = JsonConvert.DeserializeObject<AdSwitcherSettings>(description);

            if (Settings.RefreshTime > 0)
            {
                _refreshSeconds = Settings.RefreshTime;
            }


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

            StopTimer();

            FillAvailableProviders();

            if (_availableProviders.Count == 0)
            {
                //TODO: Set default banner if none defined
                Deployment.Current.Dispatcher.BeginInvoke(() => _rotatingBannerContainer.Children.Clear());
                Deployment.Current.Dispatcher.BeginInvoke(() => StartTimer());
            }
            else
            {
                //TODO: Do a draw for selecting a provider
                Deployment.Current.Dispatcher.BeginInvoke(() => SelectProvider(0));
            }
        }

        private void RefreshNext()
        {
            if (_availableProviders.Count == 0)
            {
                _rotatingBannerContainer.Children.Clear();
                StartTimer();
                return;
            }

            //When failed, select next provider in list
            SelectProvider(0);
        }

        private void RefreshProvider(IAdProvider provider)
        {
            if (_newProvider != null)
            {
                //TODO: Unanchor events
                _newProvider.PurgeControl();
            }

            if (provider == _currentProvider)
            {
                _currentProvider.New += Refresh_New;
                _currentProvider.Error += Refresh_Error;
                _currentProvider.Refresh();
                return;
            }

            _newProvider = provider;
            _newProvider.New += Refresh_New;
            _newProvider.Error += Refresh_Error;

            UIElement newAdControl = _newProvider.CreateControl();

            if (newAdControl != null)
            {
                try
                {
                    _rotatingBannerContainer.Children.Add(newAdControl);
                }
                catch (Exception)
                {
                    UnanchorNewProvider();
                    _newProvider.PurgeControl();
                    _newProvider = null;
                }
            }

            _newProvider.Start();
        }

        #region Events

        private void Refresh_New()
        {
            UnanchorNewProvider();

            if (_currentProvider != null)
            {
                _currentProvider.PurgeControl();
            }

            if (_rotatingBannerContainer.Children.Count > 0)
                _rotatingBannerContainer.Children.RemoveAt(0);

            _currentProvider = _newProvider;

            StartTimer();
        }

        private void Refresh_Error()
        {
            UnanchorNewProvider();

            if(_rotatingBannerContainer.Children.Count > 1)
                _rotatingBannerContainer.Children.RemoveAt(1);

            _newProvider.PurgeControl();
            _newProvider = null;

            RefreshNext();
        }

        #endregion

        #region Timer

        private void StartTimer()
        {
            _timer = new Timer(Refresh_Tick, null, _refreshSeconds * 1000, -1);
        }

        private void StopTimer()
        {
            _timer.Dispose();
            _timer = null;
        }

        #endregion

        #region Helpers

        private void UnanchorNewProvider()
        {
            if (_newProvider != null)
            {
                _newProvider.New -= Refresh_New;
                _newProvider.Error -= Refresh_Error;
            }
        }

        private int CompareProviders(IAdProvider provider1, IAdProvider provider2)
        {
            return provider1.GetPriority().CompareTo(provider2.GetPriority());
        }

        private void SelectProvider(int providerPosition)
        {
            if (_availableProviders.Count-1 > providerPosition)
            {
                StartTimer();
                return;
            }

            IAdProvider selectedProvider = _availableProviders[providerPosition];

            selectedProvider.New -= Refresh_New;
            selectedProvider.Error -= Refresh_Error;

            RefreshProvider(selectedProvider);
            _availableProviders.RemoveAt(providerPosition);
        }

        private void FillAvailableProviders()
        {
            _availableProviders.Clear();

            foreach (IAdProvider provider in _providers)
            {
                _availableProviders.Add(provider);
            }

            _availableProviders.Sort(CompareProviders);
        }

        #endregion

    }
}
