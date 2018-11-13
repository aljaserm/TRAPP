using Plugin.Geolocator;
using System;
using System.Linq;
using TRAPP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TRAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTravelPage : ContentPage
	{
        Post post;
		public NewTravelPage ()
		{
			InitializeComponent ();
            post = new Post();
            ContainerStackLayout.BindingContext = post;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var locator = CrossGeolocator.Current;
            var postion = await locator.GetPositionAsync();
            var venues = await Venue.GetVenues(postion.Latitude, postion.Longitude);
            lsvVenue.ItemsSource = venues.OrderBy(c => c.location.distance);
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = lsvVenue.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();

                post.VenueName = selectedVenue.name;
                post.CategoryID = firstCategory.id;
                post.CategoryName = firstCategory.name;
                post.VenueAddress = selectedVenue.location.address;
                post.VenueLat = selectedVenue.location.lat;
                post.VenueLng = selectedVenue.location.lng;
                post.VenueDistance = selectedVenue.location.distance;
                post.UserID = App.userGlobal.Id;
                Post.Insert(post);
                await DisplayAlert("Success", "Added", "OK");
                await Navigation.PushAsync(new HomePage());
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