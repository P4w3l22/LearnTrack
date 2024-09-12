using Microcharts;
using LearnTrack.MVVM.Views;
using SkiaSharp;
using Microcharts.Maui;
using LearnTrack.Services;
using LearnTrack.MVVM.ViewModels;

namespace LearnTrack.Pages;

public partial class MainPage : ContentPage
{
	CalendarViewModel _calendarViewModel;

	public MainPage(MainPageViewModel viewModel, CalendarViewModel calendarViewModel)
	{
		InitializeComponent();
		_calendarViewModel = calendarViewModel;
		BindingContext = viewModel;

		chartView.Chart = new BarChart
		{
			Entries = viewModel.Entries,
			BackgroundColor = SKColor.Parse("#202020"),
		};
	
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		DisplayAlert("Tytuł", "Test", "Ok");
	}

	private void CalendarPage_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new CalendarPage(_calendarViewModel));
	}

	private void LearnTrack_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new LearnTrackPage());
    }

}
