using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
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
                var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
                var span = new Xamarin.Forms.Maps.MapSpan(center, 2, 2);
                mpLocation.MoveToRegion(span);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var center = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 2, 2);
            mpLocation.MoveToRegion(span);
        }
    }
}