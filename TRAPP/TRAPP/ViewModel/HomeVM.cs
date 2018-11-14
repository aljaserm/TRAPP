using System;
using System.Collections.Generic;
using System.Text;
using TRAPP.ViewModel.Commands;

namespace TRAPP.ViewModel
{
    public class HomeVM
    {
        public NavigationCommand nc { get; set; }
        public HomeVM()
        {
            nc = new NavigationCommand(this);
        }

        public async void Navigate()
        {
           await App.Current.MainPage.Navigation.PushAsync(new NewTravelPage());
        }
    }
}
