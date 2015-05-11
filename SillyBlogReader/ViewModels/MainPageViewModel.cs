using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SillyBlogReader.Model;

namespace SillyBlogReader.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        protected Blogs Blogs;

        public Blog[] AllBlogs => Blogs.AllBlogs;

        protected Blog currentBlog;
        public Blog CurrentBlog
        {
            get { return currentBlog; }
            set { currentBlog = value; NotifyPropertyChanged("CurrentBlog"); }
        }

        protected BlogEntry currentEntry;
        public BlogEntry CurrentEntry
        {
            get { return currentEntry; }
            set { currentEntry = value; NotifyPropertyChanged("CurrentEntry"); }
        }

        public MainPageViewModel()
        {
            Blogs = new Blogs();
        }

        public async Task Load()
        {
            await Blogs.Load();
            CurrentBlog = AllBlogs[0];
            CurrentEntry = CurrentBlog.Entries[0];            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
