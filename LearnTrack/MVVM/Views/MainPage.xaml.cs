using LearnTrack.MVVM.ViewModels;
using LearnTrack.MVVM.Views;
using Microcharts;
using SkiaSharp;

namespace LearnTrack.Pages;

public partial class MainPage : ContentPage
{
	CalendarViewModel _calendarViewModel;
	MainPageViewModel _viewModel;

	public MainPage(MainPageViewModel viewModel, CalendarViewModel calendarViewModel)
	{
		InitializeComponent();
		_calendarViewModel = calendarViewModel;
		_viewModel = viewModel;
		BindingContext = _viewModel;

		SetCharView();
	
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_viewModel.SetEntries();
		SetCharView();
	}

	private void SetCharView()
	{
		chartView.Chart = new BarChart
		{
			Entries = _viewModel.Entries,
			BackgroundColor = SKColor.Parse("#202020"),
		};
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
