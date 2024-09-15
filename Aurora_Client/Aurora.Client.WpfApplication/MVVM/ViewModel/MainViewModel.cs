using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Aurora.Client.Communication;

namespace Aurora.Client.WpfApplication.MVVM.ViewModel
{
    internal class MainViewModel
    {
        public MainViewModel()
        {
            Communicator.Instance.ConnectToServerAsync();
        }
    }
}
