using LearnTrack.ViewModels;

namespace LearnTrack.Pages;

public partial class LearnTrackPage : TabbedPage
{
	public LearnTrackPage()
	{
		InitializeComponent();
		BindingContext = new LearnTrackViewModel();
	}
}