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

namespace AdSwitcher.Code.AdProvider
{
    public class AdProvider : IAdProvider
    {
        protected string _name = "";
        protected UIElement _control;

        public virtual UIElement CreateControl()
        {
            Debug.WriteLine("AdProvider: " + GetName() + " -> CreateControl");

            return null;
        }

        public virtual void PurgeControl()
        {
            Debug.WriteLine("AdProvider: " + GetName() + " -> PurgeControl");
        }

        public virtual void Start()
        {
            Debug.WriteLine("AdProvider: " + GetName() + " -> Start");
        }

        public virtual void Refresh()
        {
            Debug.WriteLine("AdProvider: " + GetName() + " -> Start");
        }

        public virtual string GetName()
        {
            return  _name;
        }
    }
}
