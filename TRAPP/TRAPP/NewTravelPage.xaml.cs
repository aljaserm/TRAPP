using Plugin.Geolocator;
using System;
using System.Linq;
using TRAPP.Model;
using TRAPP.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TRAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            NewTravelVM travelviewModel;

            InitializeComponent();

            travelviewModel = new NewTravelVM();
            BindingContext = travelviewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            var venues = await Venue.GetVenues(position.Latitude, position.Longitude);
            lsvVenue.ItemsSource = venues;
        }
    }
}
