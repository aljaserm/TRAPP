using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	public partial class HistoryPage : ContentPage
	{
        HistoryVM Hvm;
		public HistoryPage ()
		{
			InitializeComponent ();
            Hvm = new HistoryVM();
            BindingContext = Hvm;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Hvm.ReadHistory();
        }
    }
}