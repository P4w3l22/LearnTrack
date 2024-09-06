using CommunityToolkit.Maui.Views;
using LearnTrack.MVVM.ViewModels;
using LearnTrack.MVVM.Views;
using LearnTrack.MVVM.Views.Popups;

namespace LearnTrack.Pages;

public partial class LearnTrackPage : ContentPage
{
	LearnTrackViewModel viewModel;

	public LearnTrackPage()
	{
		InitializeComponent();
		viewModel = new();
		BindingContext = viewModel;
	}

	private void AddTopicNote_Clicked(object sender, EventArgs e)
	{
		this.ShowPopup(new AddTopicNotePopupPage(viewModel));
	}
}