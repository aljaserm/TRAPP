using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRAPP.Model;
using TRAPP.ViewModel;
using Xamarin.Forms;

namespace TRAPP
{
    public partial class MainPage : ContentPage
    {
        MainVM vM;
        public MainPage()
        {
            InitializeComponent();
            var assembly = typeof(MainPage);
            vM = new MainVM();
            BindingContext = vM;
            imgIcon.Source = ImageSource.FromResource("TRAPP.Assets.Images.plane.png", assembly);

        }
    }
}
