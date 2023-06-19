using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RSS_Reader.Lib.Interfaces;
using RSS_Reader.Lib.Models;
using RSS_Reader.Lib.Services;
using System.Collections.ObjectModel;

namespace RSS_Reader.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            this.UpdateView();
        }

        [ObservableProperty]
        ObservableCollection<RssFeed> _rssFeeds = new ObservableCollection<RssFeed>();

        [ObservableProperty]
        string _url = "";

        private IRssFeedStore _rssFeedStore = new RssFeedStore();

        [RelayCommand]
        private void AddRssFeed()
        {
            this._rssFeedStore.AddFeed(this.Url);
            this.UpdateView();
        }

        [RelayCommand]
        private void UpdateRssFeeds()
        {
            this._rssFeedStore.UpdateFeeds();
            this.UpdateView();
        }

        private void UpdateView()
        {
            this.RssFeeds.Clear();
            foreach (var feedHandler in this._rssFeedStore.GetFeeds())
                this.RssFeeds.Add(feedHandler.Feed);
        }

        [RelayCommand]
        private void ToggleThemeMode()
        {
            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }
            else 
            { 
                Application.Current.UserAppTheme = AppTheme.Dark; 
            } 
        }

        [RelayCommand]
        private void DeleteFeed(string url)
        {
            this._rssFeedStore.DeleteFeed(url);
            this.UpdateView();
        }


    }
}
