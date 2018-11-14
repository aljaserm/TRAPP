using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRAPP.Model;
using TRAPP.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TRAPP
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        RegisterVM rvm;
		public RegisterPage ()
		{
			InitializeComponent ();
            rvm = new RegisterVM();
            BindingContext = rvm;
        }
    }
}