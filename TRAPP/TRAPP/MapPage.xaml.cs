using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRAPP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;


namespace TRAPP
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
        private bool HasLocation = false;
		public MapPage ()
		{
			InitializeComponent ();
            GetPermissions();

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
                    mpLocation.IsShowingUser = true;
                    GetLocation();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error",ex.ToString(),"OK");
            }
        }

        private async void GetLocation()
        {
            if (HasLocation)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();

                MoveMap(position);
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {

            MoveMap(e.Position);
        }

        private void MoveMap(Plugin.Geolocator.Abstractions.Position position)
        {
            var center = new Position(position.Latitude, position.Longitude);
            var span = new MapSpan(center, 2, 2);
            mpLocation.MoveToRegion(span);
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

            //locator.PositionChanged += Locator_PositionChanged;

            //await locator.StartListeningAsync(TimeSpan.FromSeconds(0), 100);
                    
            //        mpLocation.MoveToRegion(span);
            //        var p = await Post.Read();
            //        DisplayinMap(p);        
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var locator = CrossGeolocator.Current;
            locator.PositionChanged -= Locator_PositionChanged;
            await locator.StopListeningAsync();
        }

        private void DisplayinMap(List<Post> post)
        {
            foreach(var p in post)
            {
                try
                {
                    var position = new Position(p.VenueLat, p.VenueLng);
                    var pin = new Pin()
                    {
                        Type = PinType.SavedPin,
                        Position = position,
                        Label = p.VenueName,
                        Address = p.VenueAddress
                    };
                    mpLocation.Pins.Add(pin);
                }
                catch(NullReferenceException nrex)
                {

                }
                catch(Exception ex)
                {

                }
            }
        }


    }
}