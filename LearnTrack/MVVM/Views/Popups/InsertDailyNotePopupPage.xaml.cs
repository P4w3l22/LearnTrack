using CommunityToolkit.Maui.Views;
using LearnTrack.MVVM.ViewModels;

namespace LearnTrack.MVVM.Views.Popups;

public partial class InsertDailyNotePopupPage : Popup
{
	public InsertDailyNotePopupPage(CalendarViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private void CloseButton_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}