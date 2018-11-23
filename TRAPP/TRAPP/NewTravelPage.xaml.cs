using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Linq;
using TRAPP.Model;
using TRAPP.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TRAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        private bool HasLocation = false;
        public NewTravelPage()
        {
            NewTravelVM travelviewModel;

            InitializeComponent();
            GetPermissions();
            travelviewModel = new NewTravelVM();
            BindingContext = travelviewModel;
        }

        private async void GetPermissions()
        {
            try
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                {
                    await DisplayAlert("Need location", "We need that location", "OK");
                }
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
                if (status != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                    if (results.ContainsKey(Permission.LocationWhenInUse))
                    {
                        status = results[Permission.LocationWhenInUse];
                    }
                }
                if (status == PermissionStatus.Granted)
                {
                    HasLocation = true;
                    //mpLocation.IsShowingUser = true;
                    GetLocation();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        private async void GetLocation()
        {
            if (HasLocation)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();

                MoveLocation(position);
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {

            MoveLocation(e.Position);
        }

        private async void MoveLocation(Plugin.Geolocator.Abstractions.Position position)
        {
            var center = new Position(position.Latitude, position.Longitude);
            var span = new MapSpan(center, 2, 2);
            var venues = await Venue.GetVenues(position.Latitude, position.Longitude);
            lsvVenue.ItemsSource = venues;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (HasLocation)
            {
                var locator = CrossGeolocator.Current;
                locator.PositionChanged += Locator_PositionChanged;
                await locator.StartListeningAsync(TimeSpan.Zero, 50);
            }
            GetLocation();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var locator = CrossGeolocator.Current;
            locator.PositionChanged -= Locator_PositionChanged;
            await locator.StopListeningAsync();
        }




        //    protected override async void OnAppearing()
        //    {
        //        base.OnAppearing();
        //        try
        //        {
        //            //var status =await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Location);
        //            //if (status != PermissionStatus.Granted)
        //            //{
        //            //    if(await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
        //            //    {
        //            //        await DisplayAlert("Need Perm","Need ur loc","ok");
        //            //    }
        //            //   var result= await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
        //            //    if (result.ContainsKey(Permission.Location))
        //            //    {
        //            //        status = result[Permission.Location];
        //            //    }
        //            //}
        //            //if (status == PermissionStatus.Granted)
        //            //{
        //            //var locator = CrossGeolocator.Current;
        //            //var position = await locator.GetPositionAsync();
        //            //var venues = await Venue.GetVenues(position.Latitude, position.Longitude);
        //            //lsvVenue.ItemsSource = venues;
        //            //base.OnAppearing();
        //            //var locator = CrossGeolocator.Current;
        //            //var postion = await locator.GetPositionAsync();
        //            //var venues = Venue.GetVenues(postion.Latitude, postion.Longitude);
        //            //lsvVenue.ItemsSource = venues;
        //            var locator = CrossGeolocator.Current;
        //            var position = await locator.GetPositionAsync();

        //            var venues = await Venue.GetVenues(position.Latitude, position.Longitude);
        //            lsvVenue.ItemsSource = venues;
        //            //    }
        //            //    else
        //            //    {
        //            //        await DisplayAlert("Need Perm", "Need ur loc", "ok");

        //            //    }
        //            //}
        //            //catch(Exception ex)
        //            //{

        //            //}
        //        }
    }
    }
