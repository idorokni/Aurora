using Aurora.Client.WpfApplication.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Client.WpfApplication.MVVM.ViewModel
{
    class SignupViewModel : ObservableObject
    {
        private string _username = "";
        private string _password = "";
        private string _confirmPassword = "";

        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(); } }
        public string ConfirmPassword { get { return _confirmPassword; } set { _confirmPassword = value; OnPropertyChanged(); } }
    }
}
