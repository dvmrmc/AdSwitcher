using System.Windows;
using System;

namespace AdSwitcher.Code.AdProvider
{
    public interface IAdProvider
    {
        event Action New;
        event Action Error;

        UIElement CreateControl();
        void PurgeControl();

        void Start();
        void Refresh();

        string GetName();

        void RaiseNew();
        void RaiseError();
    }
}
