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

        private void UpdateView()
        {
            this.RssFeeds.Clear(); // leert die Rss-Feeds
            foreach (var feedHandler in this._rssFeedStore.GetFeeds()) // da die Methode GetFeeds aus RssFeedStore.cs die
                                                                       // feedHandlers returned. werden durch die Schleife alle
                                                                       // gespeicherten Feeds wieder geaddet
                                                                       // clear wurde gemacht um die Sammlung zu löschen und komplett neu aufzusetzen
            {
                this.RssFeeds.Add(feedHandler.Feed);
            }
        }


        [ObservableProperty]
        string _url = "";


        private IRssFeedStore _rssFeedStore = new RssFeedStore(); // erstellt Private variable vom Typ IRssFeedStore
                                                                  // ermöglicht Hinzufügen, Entfernen, Abrufen von RSS-Feeds
                                                                  // dadurch können andere Teile zurückgreifen im Code


        [RelayCommand]
        private void AddRssFeed()
        {
            this._rssFeedStore.AddFeed(this.Url);               // zum Beispiel dort _rssFeedStore // Url wie entsprechend in der MainPage reingeschrieben
            this.UpdateView();                                  // Das die Änderungen in der Benutzeroberfläche übernommen werden (geupdated)
        }


        [RelayCommand]
        private void UpdateRssFeeds()
        {
            this._rssFeedStore.UpdateFeeds();                   // UpdateRssFeeds kompliziert. ruft Methode in RssFeedStore auf aktualisiert die Feeds.
                                                                // durchläuft mit einer Schleife alle RssFeedHandler die für jeden Handler die Methode UpdateFeed() auf
            this.UpdateView();                                  // Das die Änderungen in der UI (User Interface) übernommen wereden (geupdated)
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
            this._rssFeedStore.DeleteFeed(url);                 // Feed Deleted
            this.UpdateView();                                  // aktualisiert wieder das User Interface
        }


    }
}
