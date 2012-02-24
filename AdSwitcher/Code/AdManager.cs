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

namespace AdSwitcher.Code
{
    public class AdManager
    {
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
            //TODO: Initialize variables here
        }

        #endregion

    }
}
