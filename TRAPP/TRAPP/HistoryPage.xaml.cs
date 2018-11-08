using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRAPP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TRAPP
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
		public HistoryPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
            using (SQLiteConnection con = new SQLiteConnection(App.DBLocation))
            {
                con.CreateTable<Post>();
                var post = con.Table<Post>().ToList();
                lvPost.ItemsSource = post;
            }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}