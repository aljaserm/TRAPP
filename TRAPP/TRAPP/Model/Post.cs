using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TRAPP.Model
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(250)]
        public string Experience { get; set; }
        public string VenueName { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string VenueAddress { get; set; }
        public double VenueLat { get; set; }
        public double VenueLng { get; set; }
        public int VenueDistance { get; set; }
    }
}
