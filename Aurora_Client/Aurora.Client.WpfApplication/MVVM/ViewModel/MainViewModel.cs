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
        public static MainViewModel Instance { get; private set; }
        private object _currentView;
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
            Instance = this;
            CurrentView = new SignupViewModel();
            Communicator.Instance.ConnectToServerAsync();
        }
    }
}
