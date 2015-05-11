using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace SillyBlogReader.Model
{
    public class BlogEntry
    {
        public string Title { get; set; }
        public string Body { get; set; }

        public string ShortBody => CleverSubstring(Body.Replace('\n',' ').Replace('\r',' '),500);
        public BitmapImage Image { get; private set; }
        public string ImageUrl
        {
            set { if (value!=null && value!=string.Empty) Image = new BitmapImage(new Uri(value)); }
        }

        public DateTime When { get; set; }

        protected string CleverSubstring(string s, int n)
        {
            if (s.Length <= n) return s;
            while (s[n] != ' ' && n > 300) n--;
            return s.Substring(0, n) + "...";
        }

    }
}
