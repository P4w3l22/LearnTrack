using CommunityToolkit.Maui.Views;
using LearnTrack.MVVM.Models;
using LearnTrack.MVVM.ViewModels;
using LearnTrack.MVVM.Views.Popups;
using LearnTrack.Services;
using Syncfusion.Maui.Core.Carousel;

namespace LearnTrack.Pages;

public partial class CalendarPage : ContentPage
{
	CalendarViewModel _viewModel;

	public CalendarPage(CalendarViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

	private void Note_Clicked(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		DailyNote note = _viewModel.CurrentDailyNotes.FirstOrDefault(x => x.Title == button.Text);
		if (button is not null)
		{
			DisplayAlert(note.Title, note.Content, "Ok");
		}
	}

	private void AddNewDailyNote_Clicked(object sender, EventArgs e)
	{
		this.ShowPopup(new InsertDailyNotePopupPage(_viewModel));
	}

	private void DailyNoteCompletedChange_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		CheckBox checkBox = (CheckBox)sender;
		_viewModel.UpdateDailyNotes();
	}
}