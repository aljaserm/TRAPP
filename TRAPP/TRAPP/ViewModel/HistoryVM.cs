using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<bool> ReadHistory()
        {
            try
            {
                var p = await Post.Read();
                if (p != null)
                {
                    Posts.Clear();
                    foreach (var post in p)
                    {
                        Posts.Add(post);
                        //lvPost.ItemsSource = p;
                    }
                }
                //using (SQLiteConnection con = new SQLiteConnection(App.DBLocation))
                //{
                //    con.CreateTable<Post>();
                //    var post = con.Table<Post>().ToList();
                //    lvPost.ItemsSource = post;
                //}
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        public async void RemoveOldPost(Post deletePost)
        {
            await Post.Remove(deletePost);
        }
    }
}
