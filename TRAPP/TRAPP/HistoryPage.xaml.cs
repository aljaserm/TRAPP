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
        HistoryVM hvm;
		public HistoryPage ()
		{
			InitializeComponent ();
            hvm = new HistoryVM();
            BindingContext = hvm;
		}

        protected  override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                hvm.Update();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}