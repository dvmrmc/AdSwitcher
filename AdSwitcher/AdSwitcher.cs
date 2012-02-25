using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xna.Framework;
using System;
using AdSwitcher.Code;

namespace AdSwitcher
{
    public class AdSwitcher : Control
    {


        public string DefaultSettings
        {
            get { return (string)GetValue(DefaultSettingsProperty); }
            set { SetValue(DefaultSettingsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DefaultSettings.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultSettingsProperty = DependencyProperty.Register("DefaultSettings", 
                                                                                                        typeof(string), 
                                                                                                        typeof(AdSwitcher), 
                                                                                                        new PropertyMetadata(""));

        public AdSwitcher()
        {
            DefaultStyleKey = typeof(AdSwitcher);
            this.Loaded += new RoutedEventHandler(AdSwitcher_Loaded);
        }

        void AdSwitcher_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("AdSwitcher loaded");

            AdManager.Instance.Initialize();

            if (!String.IsNullOrEmpty(DefaultSettings))
            {
                //Try to load default settings from local resources
                Stream stream = TitleContainer.OpenStream(DefaultSettings);

                if (stream != null)
                {
                    StreamReader reader = new StreamReader(stream);
                    string jsonString = reader.ReadToEnd();

                    AdManager.Instance.LoadSettings(jsonString);
                    AdManager.Instance.Start();
                }
            }
        }
    }
}
