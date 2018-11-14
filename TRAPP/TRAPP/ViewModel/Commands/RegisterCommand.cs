using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TRAPP.Model;

namespace TRAPP.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        private RegisterVM reg { get; set; }
        public RegisterCommand(RegisterVM regV)
        {
            this.reg = regV;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            User user = (User)parameter;
            if (user != null)
            {
                if (user.Password == user.ConfirmPassword)
                {
                    if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public void Execute(object parameter)
        {
            User user = (User)parameter;
            reg.SignUp(user);
        }
    }
}
