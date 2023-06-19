using RSS_Reader.Lib.Models;

namespace RSS_Reader.Lib.Interfaces
{
    public interface IRssFeedHandler
    {
        RssFeed Feed { get; }

        void UpdateFeed();

        void LoadFeed(RssFeed feed);
    }
}
