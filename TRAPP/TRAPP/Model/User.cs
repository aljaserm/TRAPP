using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRAPP.Model
{
    public class User : INotifyPropertyChanged
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; onPropertyChange("Id"); }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; onPropertyChange("Email"); }
        }
        private string pass;

        public string Password
        {
            get { return pass; }
            set { pass = value; onPropertyChange("Password"); }
        }

        private string passC;

        public string ConfirmPassword
        {
            get { return passC; }
            set { passC = value; onPropertyChange("ConfirmPassword"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        //public async static Task<List<User>> Login(String Email)
        //{
        //    var u = await App.MobileService.GetTable<User>().Where(e => e.Email == Email).ToListAsync();
        //    return u;
        //}

        public async static Task<bool> UserLogin(string Email, string Password)
        {
            bool bEmail = string.IsNullOrEmpty(Email);
            bool bPass = string.IsNullOrEmpty(Password);
            if (bEmail || bPass)
            {
                return false;
            }
            else
            {
                var user = (await App.MobileService.GetTable<User>().Where(u => u.Email == Email).ToListAsync()).FirstOrDefault();
                if (user != null)
                {
                    App.userGlobal = user;
                    if (user.Password == Password)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public static async void SignUp(User user)
        {
            await App.MobileService.GetTable<User>().InsertAsync(user);
            await App.Current.MainPage.Navigation.PushAsync(new MainPage());
        }

        private void onPropertyChange(string NameProperty)
        {
            if(PropertyChanged!=null)
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
        }
    }
}
