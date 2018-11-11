using Plugin.Geolocator;
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
            try
            { 
                base.OnAppearing();
                var locator = CrossGeolocator.Current;
                locator.PositionChanged += Locator_PositionChanged;
                //The issue started when I added the following line
                await locator.StartListeningAsync(TimeSpan.FromSeconds(0), 100);
                var position =await locator.GetPositionAsync();
                var center = new Position(position.Latitude, position.Longitude);
                var span = new MapSpan(center, 2, 2);
                mpLocation.MoveToRegion(span);
                using (SQLiteConnection con = new SQLiteConnection(App.DBLocation))
                {
                    con.CreateTable<Post>();
                    var post = con.Table<Post>().ToList();
                    //lvPost.ItemsSource = post;
                    DisplayinMap(post);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }
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