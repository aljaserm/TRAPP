using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public async void Update()
        {
            
            var p = await Post.Read();
            if (p != null)
            {
                Posts.Clear();
                //lvPost.ItemsSource = p;
                foreach (var s in p)
                {
                    Posts.Add(s);
                }
            }
        }
    }
}
