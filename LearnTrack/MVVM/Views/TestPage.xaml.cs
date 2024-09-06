using CommunityToolkit.Maui.Views;

namespace LearnTrack.MVVM.Views;

public partial class TestPage : Popup
{
	public TestPage()
	{
		InitializeComponent();
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}