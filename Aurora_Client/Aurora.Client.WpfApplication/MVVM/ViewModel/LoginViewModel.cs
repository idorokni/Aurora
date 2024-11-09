using Aurora.Client.WpfApplication.Core;
using System;

namespace Aurora.Client.WpfApplication.MVVM.ViewModel
{
    internal class LoginViewModel : ObservableObject
    {
        private string _email = string.Empty;
        private string _password = string.Empty;

        public RelayCommand SwitchToSignup {  get; set; }

        public LoginViewModel()
        {
            SwitchToSignup = new RelayCommand(o =>
            {
                MainViewModel.Instance.CurrentView = new SignupViewModel();
            });
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
    }
}
