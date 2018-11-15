using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
            try
            {
                var status =await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if(await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need Perm","Need ur loc","ok");
                    }
                   var result= await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    if (result.ContainsKey(Permission.Location))
                    {
                        status = result[Permission.Location];
                    }
                }
                if (status == PermissionStatus.Granted)
                {
                    var locator = CrossGeolocator.Current;
                    var position = await locator.GetPositionAsync();
                    var venues = await Venue.GetVenues(position.Latitude, position.Longitude);
                    lsvVenue.ItemsSource = venues;
                }
                else
                {
                    await DisplayAlert("Need Perm", "Need ur loc", "ok");

                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
