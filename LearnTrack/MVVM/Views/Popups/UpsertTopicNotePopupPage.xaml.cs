using CommunityToolkit.Maui.Views;
using LearnTrack.MVVM.ViewModels;

namespace LearnTrack.MVVM.Views.Popups;

public partial class UpsertTopicNotePopupPage : Popup
{
	public UpsertTopicNotePopupPage(LearnTrackViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

	private void CloseButton_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}