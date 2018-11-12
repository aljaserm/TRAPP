using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRAPP.Logic;
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var locator = CrossGeolocator.Current;
            var postion = await locator.GetPositionAsync();
            var venues = await VenueLogic.GetVenues(postion.Latitude, postion.Longitude);
            lsvVenue.ItemsSource = venues.OrderBy(c => c.location.distance);
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = lsvVenue.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();
                Post p = new Post()
                {
                    Experience = entExperience.Text,
                    VenueName = selectedVenue.name,
                    CategoryID = firstCategory.id,
                    CategoryName = firstCategory.name,
                    VenueAddress = selectedVenue.location.address,
                    VenueLat = selectedVenue.location.lat,
                    VenueLng = selectedVenue.location.lng,
                    VenueDistance = selectedVenue.location.distance,
                    UserID=App.userGlobal.Id
                };
                await App.MobileService.GetTable<Post>().InsertAsync(p);
                await DisplayAlert("Success", "Added", "OK");
                await Navigation.PushAsync(new HomePage());

                //using (SQLiteConnection con = new SQLiteConnection(App.DBLocation))
                //{
                //    con.CreateTable<Post>();
                //    int rows = con.Insert(p);
                //    if (rows > 0)
                //    {
                //        DisplayAlert("Success", "Added", "OK");
                //        Navigation.PushAsync(new HomePage());
                //    }
                //    else
                //    {
                //        DisplayAlert("Failed", "Not Added", "OK");
                //    }
                //}
            }
            catch (NullReferenceException nrex)
            {
                await DisplayAlert("Failed", "Not Added", "OK");
            }
            catch(Exception ex)
            {
                await DisplayAlert("Failed", "Not Added", "OK");
            }
        }
    }
}