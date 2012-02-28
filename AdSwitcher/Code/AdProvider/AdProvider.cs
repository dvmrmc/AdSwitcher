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
        public event Action New;
        public event Action Error;
        public float Priority = 0;

        protected string _name = "";
        protected UIElement _control;

        protected int Width = 480;
        protected int Height = 80;

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
            Debug.WriteLine("AdProvider: " + GetName() + " -> Refresh");
        }

        public virtual string GetName()
        {
            return  _name;
        }

        public virtual float GetPriority()
        {
            return Priority;
        }

        public void RaiseNew()
        {
            Debug.WriteLine("AdProvider: " + GetName() + " -> NEW AD ACQUIRED");
            if (New != null)
                New();
            else
                Debug.WriteLine("AdProvider: " + GetName() + " -> No one handling new event");
        }

        public void RaiseError()
        {
            Debug.WriteLine("AdProvider: " + GetName() + " -> ERROR ACQUIRING AD");
            if (Error != null)
                Error();
            else
                Debug.WriteLine("AdProvider: " + GetName() + " -> No one handling error event");
        }
    }
}
