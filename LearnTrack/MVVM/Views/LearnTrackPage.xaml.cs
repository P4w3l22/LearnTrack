using LearnTrack.MVVM.ViewModels;

namespace LearnTrack.Pages;

public partial class LearnTrackPage : ContentPage
{
	public LearnTrackPage()
	{
		InitializeComponent();
		BindingContext = new LearnTrackViewModel();
	}
}