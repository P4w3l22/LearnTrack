using CommunityToolkit.Maui.Views;
using LearnTrack.MVVM.ViewModels;

namespace LearnTrack.MVVM.Views.Popups;

public partial class UpsertTopicPopupPage : Popup
{
	public UpsertTopicPopupPage(LearnTrackViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

	private void CloseButton_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}