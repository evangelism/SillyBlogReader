using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SillyBlogReader.Model
{
    public class Blogs
    {
        public Blog[] AllBlogs { get; private set; }
        public Blogs()
        {
            AllBlogs = new Blog[3];
            AllBlogs[0] = new Blog("Student", "http://blogs.msdn.com/b/rustudents/rss.aspx", Symbol.People);
            AllBlogs[1] = new Blog("Coding4Fun", "http://blogs.msdn.com/b/rucoding4fun/rss.aspx", Symbol.Emoji2);
            AllBlogs[2] = new Blog("Author", "http://blogs.msdn.com/b/sos/rss.aspx",Symbol.Contact);
        }

        public async Task Load()
        {
            foreach (var b in AllBlogs) await b.EnsureLoaded();
        }
    }
}
