using Aurora.Client.Communication;
using Aurora.Client.WpfApplication.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Client.WpfApplication.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject
    {
        private string _username;
        private string _email;

        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }
        public string Email { get => _email ; set { _email = value; OnPropertyChanged(); } }

        public RelayCommand SwitchToChangeProfile { get; set; }

        public HomeViewModel(LoggedUser user)
        {
            Username = user.Username;
            Email = user.Email;
            SwitchToChangeProfile = new RelayCommand(o =>
            {
                MainViewModel.Instance.CurrentView = new EditProfileViewModel();
            });
        }
    }
}
