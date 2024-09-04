using LearnTrack.MVVM.Models;
using LearnTrack.MVVM.ViewModels;

namespace LearnTrack.Pages;

public partial class CalendarPage : ContentPage
{
	CalendarViewModel CalendarViewModel = new();

	public CalendarPage()
	{
		InitializeComponent();
		BindingContext = CalendarViewModel;
	}

	private void Note_Clicked(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		NoteModel note = CalendarViewModel.GetNoteByTitle(button.Text);
		if (button is not null)
		{
			DisplayAlert(note.Title, note.Description, "Ok");
		}
	}

	private void CalendarCard_Clicked(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		string day = button.Text.Length == 2 ? button.Text : ("0" + button.Text);
		CalendarViewModel.ActualDate = $"{day}." + DateTime.Now.ToString("MM.yyyy");
	}
}