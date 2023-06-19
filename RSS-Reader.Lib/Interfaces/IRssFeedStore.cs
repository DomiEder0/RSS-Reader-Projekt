namespace RSS_Reader.Lib.Interfaces
{
    public interface IRssFeedStore
    {
        void AddFeed(string url);

        IEnumerable<IRssFeedHandler> GetFeeds();

        public void DeleteFeed(string url);

        public void UpdateFeeds();

    }
}
