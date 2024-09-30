using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Aurora.Client.Communication;
using Aurora.Client.WpfApplication.Core;

namespace Aurora.Client.WpfApplication.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        private object _currentView = null!;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            CurrentView = new LoginViewModel();
            Communicator.Instance.ConnectToServerAsync();
        }
    }
}
