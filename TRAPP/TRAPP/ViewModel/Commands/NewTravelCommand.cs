using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TRAPP.Model;

namespace TRAPP.ViewModel.Commands
{
    public class NewTravelCommand : ICommand
    {
        NewTravelVM tvm;
        public event EventHandler CanExecuteChanged;
        public NewTravelCommand(NewTravelVM tvm)
        {
            this.tvm = tvm;
        }
        public bool CanExecute(object parameter)
        {
            var post = (Post)parameter;
            if (post != null)
            {
                if (string.IsNullOrEmpty(post.Experience))
                    return false;
                if (post.Venue != null)
                    return true;
                return false;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            var post = (Post)parameter;
            tvm.SavePost(post);
        }
    }
}
