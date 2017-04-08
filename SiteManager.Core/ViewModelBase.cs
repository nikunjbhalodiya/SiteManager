using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManager.Core
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event Func<bool> MessageBoxEvent;

        protected bool OnMessageBoxEvent()
        {
            if (MessageBoxEvent == null) {
                return false;
            }
            return MessageBoxEvent();
        }
    }
}
