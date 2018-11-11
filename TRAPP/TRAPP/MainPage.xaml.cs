using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TRAPP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var assembly = typeof(MainPage);
            imgIcon.Source = ImageSource.FromResource("TRAPP.Assets.Images.plane.png", assembly);

        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            bool bEmail=string.IsNullOrEmpty(entEmail.Text);
            bool bPass=string.IsNullOrEmpty(entPass.Text);

            if(bEmail || bPass)
            {

            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }
        }
    }
}
