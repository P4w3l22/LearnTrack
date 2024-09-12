using LearnTrack.MVVM.Models;
using LearnTrack.MVVM.ViewModels;
using LearnTrack.Pages;
using LearnTrack.Repositories;
using LearnTrack.Repositories.IRepository;
using LearnTrack.Services;

namespace LearnTrack;

public partial class App : Application
{
	public static IBaseRepository<Subject> SubjectRepository { get; set; }
	public static IBaseRepository<Topic> TopicRepository { get; set; }
	public static IBaseRepository<TopicNote> TopicNoteRepository { get; set; }
	public static IBaseRepository<DailyNote> DailyNoteRepository { get; set; }

	public App(IBaseRepository<Subject> subjectRepository, IBaseRepository<Topic> topicRepository,
			   IBaseRepository<TopicNote> topicNoteRepository, IBaseRepository<DailyNote> dailyNoteRepository,
			   MainPageViewModel mainPageViewModel, CalendarViewModel calendarViewModel)
	{
		InitializeComponent();

		SubjectRepository = subjectRepository;
		TopicRepository = topicRepository;
		TopicNoteRepository = topicNoteRepository;
		DailyNoteRepository = dailyNoteRepository;

		MainPage = new NavigationPage(new MainPage(mainPageViewModel, calendarViewModel));
	}
}
