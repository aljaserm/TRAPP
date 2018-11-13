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
            bool Authorized = await User.UserLogin(entEmail.Text,entPass.Text);
            if (Authorized)
            {
                await Navigation.PushAsync(new HomePage());
            }
            else
            {

                await DisplayAlert("Error", "Email or Password are not correct", "OK");
            }
        }

        private void btnSignupUser_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
