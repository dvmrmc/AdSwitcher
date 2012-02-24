using System.Windows;
using System;

namespace AdSwitcher.Code.AdProvider
{
    public interface IAdProvider
    {
        Action New;
        Action Error;

        UIElement CreateControl();
        void PurgeControl();

        void Start();
        void Refresh();

        string GetName();
    }
}
