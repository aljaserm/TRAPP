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
        public ProfilePage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //    using (SQLiteConnection con = new SQLiteConnection(App.DBLocation))
            //    {
            var postTable = await Post.Read();

            var CategoryCount = Post.CategoryDictionry(postTable);
            lvCategory.ItemsSource = CategoryCount;
            lblPostCount.Text = postTable.Count.ToString();
            //    }
            //}
        }
    }
}