using System;
using System.Text.Json;
using System.Threading.Tasks;
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
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Instance = this;
            InitializeViewModelAsync();
        }

        private async void InitializeViewModelAsync()
        {
            Communicator.Instance.ConnectToServerAsync();

            var responseInfo = await AuthenticationManagerAurora.Instance.TrySigninginToServerWithToken();
            if (responseInfo.code == ResponseCode.TOKEN_CONNECT_FAILED || string.IsNullOrEmpty(responseInfo.message))
            {
                CurrentView = new LoginViewModel();
            }
            else
            {
                var loggedUser = JsonSerializer.Deserialize<LoggedUser>(responseInfo.message);
                CurrentView = new HomeViewModel(loggedUser);
            }
        }
    }
}
