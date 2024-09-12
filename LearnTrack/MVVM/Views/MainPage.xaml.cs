using Microcharts;
using LearnTrack.MVVM.Views;
using SkiaSharp;
using Microcharts.Maui;
using LearnTrack.Services;
using LearnTrack.MVVM.ViewModels;

namespace LearnTrack.Pages;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
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
