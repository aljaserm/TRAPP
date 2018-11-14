using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Linq;
using TRAPP.Model;

namespace TRAPP.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public MainVM main { get; set; }
        public LoginCommand(MainVM mainVm)
        {
            main = mainVm;
        }
        public event EventHandler CanExecuteChanged;

     
        public bool CanExecute(object parameter)
        {
            var user = (User)parameter;
            if (user == null)
                return false;

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                return false;
            return true;
        }

        public void Execute(object parameter)
        {
            main.Login();
        }
    }
}
