using LearnTrack.MVVM.Models;
using LearnTrack.Pages;
using LearnTrack.Repositories;

namespace LearnTrack;

public partial class App : Application
{
	public static BaseRepository<Subject> SubjectRepository { get; set; }
	public static BaseRepository<Topic> TopicRepository { get; set; }
	public static BaseRepository<TopicNote> TopicNoteRepository { get; set; }
	public static BaseRepository<DailyNote> DailyNoteRepository { get; set; }

	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new MainPage());
	}
}
