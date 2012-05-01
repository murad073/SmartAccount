using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GKS.Model
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler<EventArgs> OnFinish;
        public void InvokeOnFinish(EventArgs e = null)
        {
            EventHandler<EventArgs> handler = OnFinish;
            if (handler != null) handler(this, e ?? new EventArgs());
        }
    }
}
