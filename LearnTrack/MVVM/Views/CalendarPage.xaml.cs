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

		Month_Button.Text = Month_Picker.SelectedItem.ToString();
	}

	private void Month_Picker_SelectedIndexChanged(object sender, EventArgs e)
	{
		Month_Button.Text = Month_Picker.SelectedItem.ToString();
	}
}