using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRAPP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TRAPP
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
		}

        private async void btnSignup_Clicked(object sender, EventArgs e)
        {
            if (entPass.Text == entPassConfirm.Text)
            {
                User u = new User()
                {
                    Email = entEmail.Text,
                    Password = entPass.Text
                };
                await App.MobileService.GetTable<User>().InsertAsync(u);
            }
            else
            {
               await DisplayAlert("Error", "Password doesn't match", "OK");
            }
        }
    }
}