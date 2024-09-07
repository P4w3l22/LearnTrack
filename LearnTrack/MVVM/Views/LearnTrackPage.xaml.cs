using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using LearnTrack.MVVM.ViewModels;
using LearnTrack.MVVM.Views;
using LearnTrack.MVVM.Views.Popups;

namespace LearnTrack.MVVM.Views;

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
		this.ShowPopup(new UpsertTopicNotePopupPage(viewModel));
	}

	private void UpsertTopic_Clicked(object sender, EventArgs e)
	{
		this.ShowPopup(new UpsertTopicPopupPage(viewModel));
	}

	private async void Button_ClickedAsync(object sender, EventArgs e)
	{
		bool answer = await DisplayAlert("Potwierdzenie", "Czy chcesz usun¹æ?", "Tak", "Nie");

		if (answer)
		{
			viewModel.DeleteTopicCommand.Execute(null);
			await DisplayAlert("Potwierdzenie", "Usuniêto pomyœlnie!", "OK");
		}
		else
		{
			// Operacja odrzucona przez u¿ytkownika
			await DisplayAlert("Odrzucono", "Anulowano.", "OK");
		}
	}

	private void UpsertSubject_Clicked(object sender, EventArgs e)
	{
		this.ShowPopup(new UpsertSubjectPopupPage(viewModel));
	}

	private void UpDelSubject_Clicked(object sender, EventArgs e)
	{
		this.ShowPopup(new UpDelSubjectPopupPage(viewModel));
	}
}