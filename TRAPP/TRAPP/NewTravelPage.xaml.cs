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
	public partial class NewTravelPage : ContentPage
	{
		public NewTravelPage ()
		{
			InitializeComponent ();
		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Post p = new Post();
            {
                p.Experience = entExperience.Text;
            };

            using (SQLiteConnection con = new SQLiteConnection(App.DBLocation))
            {
                con.CreateTable<Post>();
                int rows = con.Insert(p);
                if (rows > 0)
                {
                    DisplayAlert("Success", "Added", "OK");
                }
                else
                {
                    DisplayAlert("Failed", "Not Added", "OK");
                }
            }
        }
    }
}