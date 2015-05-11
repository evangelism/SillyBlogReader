using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Data.Html;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;

namespace SillyBlogReader.Model
{
    public class Blog
    {
        public BlogEntry[] Entries { get; private set; }
        public string Title { get; private set; }
        public string ShortTitle { get; private set; }
        public BitmapImage Image { get; private set; }
        public Symbol Symbol { get; set; }

        public string ImageUrl
        {
            set { Image = new BitmapImage(new Uri(value)); }
        }

        public string Url { get; private set; }

        public Blog(string title, string url, Symbol S)
        {
            ShortTitle = title;
            Url = url;
            Symbol = S;
        }

        public bool IsLoaded { get; private set; } = false;

        public async Task Load()
        {
            var cli = new HttpClient();
            var s = await cli.GetInputStreamAsync(new Uri(Url));
            var xdoc = XDocument.Load(s.AsStreamForRead()); // XDocument.Parse(s);
            Title = (from x in xdoc.Descendants("title")
                     select x.Value).First();
            var res = from x in xdoc.Descendants("item")
                      select new BlogEntry()
                      {
                          Title = x.Element("title").Value,
                          Body = HtmlUtilities.ConvertToText(x.Element("description").Value),
                          ImageUrl = GetImageUrl(x.Element("description").Value)
                      };
            Entries = res.ToArray();
            IsLoaded = true;
        }

        public async Task EnsureLoaded()
        {
            if (!IsLoaded) await Load();
        }

        private static string GetImageUrl(string text)
        {
            return Regex.Matches(text,
                    @"(?<=<img\s+[^>]*?src=(?<q>['""]))(?<url>.+?)(?=\k<q>)",
                    RegexOptions.IgnoreCase)
                .Cast<Match>()
                .Where(m =>
                {
                    Uri url;
                    if (Uri.TryCreate(m.Groups[0].Value, UriKind.Absolute, out url))
                    {
                        string ext = Path.GetExtension(url.AbsolutePath).ToLower();
                        if (ext == ".png" || ext == ".jpg" || ext == ".bmp") return true;
                    }
                    return false;
                })
                .Select(m => m.Groups[0].Value)
                .FirstOrDefault();
        }

    }
}
