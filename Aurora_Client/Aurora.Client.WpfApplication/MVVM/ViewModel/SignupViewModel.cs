using Aurora.Client.Communication;
using Aurora.Client.WpfApplication.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Aurora.Client.WpfApplication.MVVM.ViewModel
{
    class SignupViewModel : ObservableObject
    {
        private string _email = "";
        private string _username = "";
        private string _errorString = "";
        private string _password = "";
        private string _confirmPassword = "";

        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }
        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(); } }
        public string ErrorString { get { return _errorString; } set { _errorString = value; OnPropertyChanged(); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(); } }
        public string ConfirmPassword { get { return _confirmPassword; } set { _confirmPassword = value; OnPropertyChanged(); } }

        public RelayCommand SubmitSignupCommand { get; set; }
        public RelayCommand SwitchToSignin { get; set; }

        public SignupViewModel()
        {
            SubmitSignupCommand = new RelayCommand(async o =>
            {
                var serverResponse = await AuthenticationManagerAurora.Instance.SignupToServer(_username, _password, _email);
                if(serverResponse.code == ResponseCode.TOKEN_SIGNUP_FAILED)
                {
                    ErrorString = serverResponse.message;
                }
                else
                {
                    MainViewModel.Instance.CurrentView = new HomeViewModel();
                }
            }, o => _confirmPassword != string.Empty && _password != string.Empty && _confirmPassword == _password);

            SwitchToSignin = new RelayCommand(o =>
            {
                MainViewModel.Instance.CurrentView = new LoginViewModel();
            });
        }
    }
}
