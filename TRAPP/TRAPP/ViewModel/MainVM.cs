using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TRAPP.Model;
using TRAPP.ViewModel.Commands;

namespace TRAPP.ViewModel
{
    public class MainVM:INotifyPropertyChanged
    {
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
        public SignupCommand SignupCommand { get; set; }
        public LoginCommand LoginCommand { get; set; }
        private string email;

        public string Email
        {
            get { return email; }
            set {
                email = value;
                User = new User()
                {
                    Email = this.Email,
                    Password = this.Password
                };
                onPropertyChange("Email"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string pass;


        public string Password
        {
            get { return pass; }
            set {
                pass = value;
                User = new User()
                {
                    Email = this.Email,
                    Password = this.Password
                };
                onPropertyChange("Password"); }
        }


        public MainVM()
        {
            User = new User();
            LoginCommand = new LoginCommand(this);
            SignupCommand = new SignupCommand(this);
        }

        public async void Login()
        {
            bool Authorized = await User.UserLogin(User.Email, User.Password);
            if (Authorized)
            {
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Error", "Email or Password are not correct", "OK");
            }
        }

        private void onPropertyChange(string NameProperty)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
        }
        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }
}
