using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRAPP.Model;
using Xamarin.Forms;

namespace TRAPP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var assembly = typeof(MainPage);
            imgIcon.Source = ImageSource.FromResource("TRAPP.Assets.Images.plane.png", assembly);

        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            bool bEmail=string.IsNullOrEmpty(entEmail.Text);
            bool bPass=string.IsNullOrEmpty(entPass.Text);
            if(bEmail || bPass)
            {

            }
            else
            {
                var user = (await App.MobileService.GetTable<User>().Where(u => u.Email == entEmail.Text).ToListAsync()).FirstOrDefault();
                if (user != null)
                {
                    App.userGlobal = user;
                    if (user.Password == entPass.Text)
                    {
                        await Navigation.PushAsync(new HomePage());

                    }
                    else
                    {
                        await DisplayAlert("Error", "Password not correct", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Emil not correct", "OK");
                }
            }
        }

        private void btnSignupUser_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
