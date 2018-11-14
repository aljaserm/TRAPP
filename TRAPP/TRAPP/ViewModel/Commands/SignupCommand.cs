using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TRAPP.ViewModel.Commands
{
    public class SignupCommand: ICommand
    {
        private MainVM main { get; set; }
        public SignupCommand(MainVM mainV)
        {
            main = mainV;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            main.Navigate();
        }
    }
}
