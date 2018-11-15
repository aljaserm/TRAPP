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
		public MapPage ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    var locator = CrossGeolocator.Current;
                    locator.PositionChanged += Locator_PositionChanged;
                    await locator.StartListeningAsync(TimeSpan.FromSeconds(0), 100);
                    var position = await locator.GetPositionAsync();
                    var center = new Position(position.Latitude, position.Longitude);
                    var span = new MapSpan(center, 2, 2);
                    mpLocation.MoveToRegion(span);
                    var p = await Post.Read();
                    DisplayinMap(p);
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }


         
               // if (status == PermissionStatus.Granted)
                //{


                //}
               // else
                //{
                  //  await DisplayAlert("Need Perm", "Need ur loc", "ok");
                //}

        
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

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var center = new Position(e.Position.Latitude, e.Position.Longitude);
            var span = new MapSpan(center, 2, 2);
            mpLocation.MoveToRegion(span);
        }
    }
}