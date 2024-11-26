using Aurora.Client.Communication;
using Aurora.Client.WpfApplication.Core;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows;

namespace Aurora.Client.WpfApplication.MVVM.ViewModel
{
    internal class LoginViewModel : ObservableObject
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorString = string.Empty;
        private Visibility _errorVisibility = Visibility.Collapsed;

        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(); } }
        public string ErrorString { get { return _errorString; } set { _errorString = value; OnPropertyChanged(); } }
        public Visibility ErrorVisibility { get { return _errorVisibility; } set { _errorVisibility = value; OnPropertyChanged(); } }

        public RelayCommand SwitchToSignup {  get; set; }
        public RelayCommand SubmitSigninCommand { get; set; }

        public LoginViewModel()
        {
            SwitchToSignup = new RelayCommand(o =>
            {
                MainViewModel.Instance.CurrentView = new SignupViewModel();
            });

            SubmitSigninCommand = new RelayCommand(async o =>
            {
                var serverResponse = await AuthenticationManagerAurora.Instance.SigninToServer(_username, _password);
                if (serverResponse.code == ResponseCode.TOKEN_LOGIN_FAILED)
                {
                    ErrorString = serverResponse.message;
                    ErrorVisibility = Visibility.Visible;
                    return;
                }
                var loginReturnData = JsonSerializer.Deserialize<LoginReturnData>(serverResponse.message);
                await TokenManager.Instance.SaveTokenToFileAsync(loginReturnData.Token);
                MainViewModel.Instance.CurrentView = new HomeViewModel( new LoggedUser(_username, loginReturnData.Email));
            }, o => _username != string.Empty && _password != string.Empty);

        }
    }
}
