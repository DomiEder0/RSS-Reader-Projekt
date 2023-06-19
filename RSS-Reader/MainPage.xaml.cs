using RSS_Reader.ViewModels;

namespace RSS_Reader;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();
	}
}

