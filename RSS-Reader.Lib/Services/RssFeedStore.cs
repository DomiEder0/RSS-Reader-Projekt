using RSS_Reader.Lib.Interfaces;
using RSS_Reader.Lib.Models;
using System.Xml.Linq;

namespace RSS_Reader.Lib.Services
{
    public class RssFeedStore : IRssFeedStore
    {
        private string _pathToSavedFile;
        private int _updateIntervall = 10;

        public RssFeedStore()
        {
            string fileName = "feeds.xml";
            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            this._pathToSavedFile = userDirectory + "\\" + fileName;

            if (File.Exists(this._pathToSavedFile))
                LoadFeedsFromXml();
        }

        private readonly List<IRssFeedHandler> feedHandlers = new List<IRssFeedHandler>();

        public void AddFeed(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var handler = new RssFeedHandler(url); 
                feedHandlers.Add(handler); // handler komponent der spezielle Aufgaben
            }
        }

        public void UpdateFeeds()
        {
            foreach (var handler in feedHandlers)
            {
                handler.UpdateFeed();
            }
            SaveFeedsToXml();
        }

        public void DeleteFeed(string url)
        {
            var feedToDelete = feedHandlers.FirstOrDefault(fh => fh.Feed.Link == url);

            if (feedToDelete != null)
            {
                feedHandlers.Remove(feedToDelete);
            }
            this.SaveFeedsToXml();
        }

        public IEnumerable<IRssFeedHandler> GetFeeds()
        {
            this.SaveFeedsToXml();
            return feedHandlers;
        }

        private void SaveFeedsToXml()
        {
            try
            {
                var feeds = feedHandlers.Select(h => h.Feed).ToList();

                var xDoc = new XDocument(
                    new XElement("Feeds",
                        from feed in feeds
                        select new XElement("Feed",
                            new XElement("Title", feed.Title),
                            new XElement("Link", feed.Link),
                            new XElement("Description", feed.Description),
                            new XElement("Items",
                                from item in feed.Items
                                select new XElement("Item",
                                    new XElement("Title", item.Title),
                                    new XElement("Link", item.Link),
                                    new XElement("Description", item.Description),
                                    new XElement("PublishDate", item.PublishDate),
                                    new XElement("New", item.New),
                                    new XElement("Selected", item.Selected)
                                )
                            )
                        )
                    )
                );

                xDoc.Save(this._pathToSavedFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void LoadFeedsFromXml()
        {
            var xDoc = XDocument.Load(this._pathToSavedFile);
            feedHandlers.Clear();

            foreach (var feed in xDoc.Descendants("Feed"))
            {
                var handler = new RssFeedHandler((string)feed.Element("Link"));
                handler.LoadFeed(new RssFeed
                {
                    Title = (string)feed.Element("Title"),
                    Link = (string)feed.Element("Link"),
                    Description = (string)feed.Element("Description"),
                    Items = feed.Element("Items").Elements("Item")
                        .Select(item => new RssFeedItem
                        {
                            Title = (string)item.Element("Title"),
                            Link = (string)item.Element("Link"),
                            Description = (string)item.Element("Description"),
                            PublishDate = DateTimeOffset.Parse((string)item.Element("PublishDate")),
                            New = bool.Parse((string)item.Element("New")),
                            Selected = bool.Parse((string)item.Element("Selected"))
                        }).ToList()
                });

                feedHandlers.Add(handler);
            }
        }

    }
}

