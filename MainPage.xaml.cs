using CarListApp.Maui.ViewModels;
namespace CarListApp.Maui;

public partial class MainPage : ContentPage
{
    //private readonly CarListViewModel carListViewModel;
    //int count = 0;

	public MainPage(CarViewModel carListViewModel)
	{
		InitializeComponent();
        BindingContext = carListViewModel;

        Preferences.Set("saveDetails", true);
        var detailsSaved = Preferences.Get("saveDetails", false);
    }

    
}

