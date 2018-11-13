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
        User user;
		public RegisterPage ()
		{
			InitializeComponent ();
            user = new User();
            ContainerStackLayout.BindingContext = user;
		}

        private async void btnSignup_Clicked(object sender, EventArgs e)
        {
            if (entPass.Text == entPassConfirm.Text)
            {
                User.SignUp(user);
            }
            else
            {
               await DisplayAlert("Error", "Password doesn't match", "OK");
            }
        }
    }
}