
using LearnTrack.MVVM.Views;

namespace LearnTrack.Pages;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		DisplayAlert("Tytuł", "Test", "Ok");
	}

	private void CalendarPage_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new CalendarPage());
	}

	private void LearnTrack_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new LearnTrackPage());
    }

	private void GetSubjects()
	{

	}

}
