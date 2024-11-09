using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Server.Communication
{
    internal class SignupData
    {
        private string _username;
        private string _password;
        private string _email;

        public string Email { get { return _email; } set { _email = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public string Username { get { return _username; } set {_username = value; } }
    }
}
