using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using TRAPP.Model;

namespace TRAPP.ViewModel
{
    public class HistoryVM
    {
        public ObservableCollection<Post> Posts { get; set; }

        public HistoryVM()
        {
            Posts = new ObservableCollection<Post>();
        }

        public async void ReadHistory()
        {
            try
            {
                var p = await Post.Read();
                if (p != null)
                {
                    Posts.Clear();
                    foreach (var pt in p)
                    {
                        Posts.Add(pt);
                        //lvPost.ItemsSource = p;
                    }
                }
                //using (SQLiteConnection con = new SQLiteConnection(App.DBLocation))
                //{
                //    con.CreateTable<Post>();
                //    var post = con.Table<Post>().ToList();
                //    lvPost.ItemsSource = post;
                //}
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
