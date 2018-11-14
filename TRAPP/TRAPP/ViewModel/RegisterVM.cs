using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TRAPP.Model;
using TRAPP.ViewModel.Commands;

namespace TRAPP.ViewModel
{
   public class RegisterVM: INotifyPropertyChanged
    {
      

     
        public RegisterVM()
        {
            RegisterCommand = new RegisterCommand(this);

        }
        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                onPropertyChange("User");
            }
        }
        private RegisterCommand rc;
        public RegisterCommand RegisterCommand
        {
            get { return rc; }
            set
            {
                rc = value;
                onPropertyChange("RegisterCommand");
            }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set {
                email = value;
                User = new User()
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
                onPropertyChange("Email");

            }
        }

        private string pass;

        public string Password
        {
            get { return pass; }
            set {
                pass = value;
                User = new User()
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
                onPropertyChange("Password");
            }
        }
        private string passC;

        public string ConfirmPassword
        {
            get { return passC; }
            set
            {
                passC = value;
                User = new User()
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
                onPropertyChange("ConfirmPassword");
            }
        }

        public  void SignUp(User user)
        {
            User.SignUp(user);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChange(string NameProperty)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
        }
    }
}
