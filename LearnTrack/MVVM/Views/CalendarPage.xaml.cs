using CommunityToolkit.Maui.Views;
using LearnTrack.MVVM.Models;
using LearnTrack.MVVM.ViewModels;
using LearnTrack.MVVM.Views.Popups;
using Syncfusion.Maui.Core.Carousel;

namespace LearnTrack.Pages;

public partial class CalendarPage : ContentPage
{
	CalendarViewModel viewModel = new();

	public CalendarPage()
	{
		InitializeComponent();
		BindingContext = viewModel;

		//Month_Button.Text = Month_Picker.SelectedItem.ToString();
	}

	private void Month_Picker_SelectedIndexChanged(object sender, EventArgs e)
	{
		//if (Month_Picker.SelectedItem is null)
		//{
		//	Month_Picker.SelectedIndex = DateTime.Now.Month - 1;
		//	Month_Picker.SelectedItem = Month_Picker.SelectedIndex;
		//}
		//Month_Button.Text = Month_Picker.SelectedItem.ToString();
	}

	private void Note_Clicked(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		DailyNote note = viewModel.CurrentDailyNotes.FirstOrDefault(x => x.Title == button.Text);
		if (button is not null)
		{
			DisplayAlert(note.Title, note.Content, "Ok");
		}
	}

	private void AddNewDailyNote_Clicked(object sender, EventArgs e)
	{
		this.ShowPopup(new InsertDailyNotePopupPage(viewModel));
	}

	private void DailyNoteCompletedChange_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		CheckBox checkBox = (CheckBox)sender;
		viewModel.UpdateDailyNote();
	}
}