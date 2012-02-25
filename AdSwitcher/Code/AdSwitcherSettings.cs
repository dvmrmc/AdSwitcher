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
using System.Collections.Generic;
using AdSwitcher.Code.AdProvider;

namespace AdSwitcher.Code
{
    public class AdSwitcherSettings
    {
        public string Test = "";
        public int RefreshTime = 0;

        public List<AdProviderSettings> Providers;
    }
}
