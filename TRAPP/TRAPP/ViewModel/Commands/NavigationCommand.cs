using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TRAPP.ViewModel.Commands
{
   public class NavigationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public HomeVM Homevm { get; set; }

        public NavigationCommand(HomeVM hvm)
        {
            Homevm = hvm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Homevm.Navigate();
        }
    }
}
