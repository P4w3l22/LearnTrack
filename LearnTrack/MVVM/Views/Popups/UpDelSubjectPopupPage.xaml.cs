using CommunityToolkit.Maui.Views;
using LearnTrack.MVVM.ViewModels;

namespace LearnTrack.MVVM.Views.Popups;

public partial class UpDelSubjectPopupPage : Popup
{
	public UpDelSubjectPopupPage(LearnTrackViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

	private void CloseButton_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}