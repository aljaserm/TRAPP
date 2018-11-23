using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRAPP.Helpers;
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Hvm.ReadHistory();

           await AzureAppServiceHelper.SyncAsync();
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var post =(Post) ((MenuItem)sender).CommandParameter;
            Hvm.RemoveOldPost(post);

            Hvm.ReadHistory();
        }

        private async void LvPost_Refreshing(object sender, EventArgs e)
        {
            await Hvm.ReadHistory();
            await AzureAppServiceHelper.SyncAsync();
            lvPost.IsRefreshing = false;
        }
    }
}