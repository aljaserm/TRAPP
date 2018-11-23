using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel;
using Newtonsoft.Json;

namespace TRAPP.Model
{
    public class Post:INotifyPropertyChanged
    {

        private string id;

        public string  ID
        {
            get { return id; }
            set { id = value; onPropertyChange("ID"); }
        }
        private string experience;

        public string Experience
        {
            get { return experience; }
            set { experience = value; onPropertyChange("Experience"); }
        }

        private string venueName;

        public  string VenueName
        {
            get { return venueName; }
            set { venueName = value; onPropertyChange("VenueName"); }
        }

        private string categoryId;

        public string CategoryID
        {
            get { return categoryId; }
            set { categoryId = value; onPropertyChange("CategoryID"); }
        }


        private string categoryName;

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; onPropertyChange("categoryName"); }
        }
        private string venueAddress;

        public string VenueAddress
        {
            get { return venueAddress; }
            set { venueAddress = value; onPropertyChange("VenueAddress"); }
        }

        private double venueLat;

        public double VenueLat
        {
            get { return venueLat; }
            set { venueLat = value; onPropertyChange("VenueLat"); }
        }
        private double venueLong;

        public double VenueLng
        {
            get { return venueLong; }
            set { venueLong = value; onPropertyChange("VenueLng"); }
        }
        private int venueDistance;

        public int VenueDistance
        {
            get { return venueDistance; }
            set { venueDistance = value; onPropertyChange("VenueDistance"); }
        }
        private string userID;

        public string UserID
        {
            get { return userID; }
            set { userID = value; onPropertyChange("UserID"); }
        }

        private DateTimeOffset createdat;

        public DateTimeOffset CREATEDAT
        {
            get { return createdat; }
            set { createdat = value; onPropertyChange("CREATEDAT"); }
        }
        private Venue venue;
        [JsonIgnore]
        public Venue Venue
        {
            get { return venue; }
            set
            {
                venue = value;
                if (venue.categories != null)
                {
                    var firstCategory = venue.categories.FirstOrDefault();
                
                if (firstCategory != null)
                {
                    CategoryID = firstCategory.id;
                    CategoryName = firstCategory.name;
                }
                }
                if (venue.location != null)
                {
                    VenueAddress = venue.location.address;
                    VenueLat = venue.location.lat;
                    VenueLng = venue.location.lng;
                    VenueDistance = venue.location.distance;
                }
                VenueName = venue.name;
                UserID = App.userGlobal.Id;
                onPropertyChange("Venue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static async void Insert(Post post)
        {
            await App.MobileService.GetTable<Post>().InsertAsync(post);
        }

        public static async Task<bool> Remove(Post post)
        {
            try
            {
                await App.MobileService.GetTable<Post>().DeleteAsync(post);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static async Task<List<Post>> Read()
        {
           var read= await App.MobileService.GetTable<Post>().Where(x => x.UserID == App.userGlobal.Id).ToListAsync();
            return read;
        }
        public static Dictionary<string,int> CategoryDictionry(List<Post> posts)
        {
            var category = (from p in posts orderby p.CategoryID select p.CategoryName).Distinct().ToList();
            Dictionary<string, int> CategoryCount = new Dictionary<string, int>();
            foreach (var c in category)
            {
                if(c!= null)
                {
                    var count = (from p in posts where p.CategoryName == c select p).ToList().Count;
                    CategoryCount.Add(c, count);
                }

            }
            return CategoryCount;
        }

        private void onPropertyChange(string NameProperty)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
        }
    }
}
