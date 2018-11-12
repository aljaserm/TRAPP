using SQLite;
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
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection con = new SQLiteConnection(App.DBLocation))
            {
                var postTable = con.Table<Post>().ToList();
                var category = (from p in postTable orderby p.CategoryID select p.CategoryName).Distinct().ToList();
                Dictionary<string,int> CategoryCount= new Dictionary<string, int>();
                foreach(var c in category)
                {
                    if (c != null) {
                        var count = (from p in postTable where p.CategoryName == c select p).ToList().Count;
                        CategoryCount.Add(c, count);
                    }
                    else
                    {
                        var count = (from p in postTable where p.CategoryName == null select p).ToList().Count;
                        CategoryCount.Add("NO Category", count);
                    }
                }

                lvCategory.ItemsSource = CategoryCount;
                lblPostCount.Text = postTable.Count.ToString();
            }
        }
    }
}