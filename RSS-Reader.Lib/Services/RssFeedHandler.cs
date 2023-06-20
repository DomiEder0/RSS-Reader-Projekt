using RSS_Reader.Lib.Interfaces;
using RSS_Reader.Lib.Models;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RSS_Reader.Lib.Services
{
    public class RssFeedHandler : IRssFeedHandler
    {
        private readonly string url;

        public RssFeed Feed { get; private set; }

        public RssFeedHandler(string url)
        {
            this.url = url;
            this.UpdateFeed();
        }

        public void LoadFeed(RssFeed feed)
        {
            Feed = feed;
        }

        public void UpdateFeed()
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Parse;
                using (XmlReader reader = XmlReader.Create(url, settings))
                {
                    var feed = SyndicationFeed.Load(reader);

                    var newFeed = new RssFeed           
                    {
                        Title = feed.Title.Text,                
                        Link = feed.Links[0].Uri.ToString(),
                        Description = feed.Description.Text,
                        Items = feed.Items.Select(item => new RssPost
                        {
                            Title = item.Title.Text,
                            Link = item.Links[0].Uri.ToString(),
                            Description = item.Summary.Text,
                            PublishDate = item.PublishDate,
                            New = Feed == null || !Feed.Items.Any(i => i.Link == item.Links[0].Uri.ToString()),
                            Selected = false
                        }).ToList()
                    };

                    Feed = newFeed;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);      
            }
        }

    }
}
