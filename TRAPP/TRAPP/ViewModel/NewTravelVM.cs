using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TRAPP.Model;
using TRAPP.ViewModel.Commands;

namespace TRAPP.ViewModel
{
    public class NewTravelVM: INotifyPropertyChanged
    {
        public NewTravelCommand NewTravelCommand { get; set; }

        private Post post;

        public Post Post
        {
            get { return post; }
            set { post = value; onPropertyChange("Post"); }
        }
        private string experience;

        public string Experience
        {
            get { return experience; }
            set
            {
                experience = value;
                Post = new Post()
                {
                    Experience=this.Experience,
                    Venue=this.Venue
                };
                onPropertyChange("Experience");
            }
        }

        private Venue venue;

        public Venue Venue
        {
            get { return venue; }
            set
            {
                venue = value;
                Post = new Post()
                {
                    Experience = this.Experience,
                    Venue = this.Venue
                };
                onPropertyChange("Venue");
            }
        }


        public NewTravelVM()
        {
            NewTravelCommand = new NewTravelCommand(this);
            Venue = new Venue();
            Post = new Post();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChange(string NameProperty)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
        }
        public async void SavePost(Post post)
        {
            try
            {
                Post.Insert(post);
                await App.Current.MainPage.DisplayAlert("Success", "Added", "OK");
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            catch (NullReferenceException nrex)
            {
                await  App.Current.MainPage.DisplayAlert("Failed", "Not Added", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Failed", "Not Added", "OK");
            }
        }
    }
}
