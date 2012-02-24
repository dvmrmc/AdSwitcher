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
using System.Diagnostics;

namespace AdSwitcher
{
    public class AdSwitcher : Control
    {
        public AdSwitcher()
        {
            DefaultStyleKey = typeof(AdSwitcher);
            this.Loaded += new RoutedEventHandler(AdSwitcher_Loaded);
        }

        void AdSwitcher_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("AdSwitcher loaded");
        }
    }
}
