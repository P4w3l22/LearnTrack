using LearnTrack.MVVM.Models;
using LearnTrack.Repositories.IRepository;

namespace LearnTrack.Services;

public class StatisticsService : IStatisticsService
{
	private readonly IBaseRepository<DailyNote> _dailyNoteRepository;
	private readonly IBaseRepository<Subject> _subjectRepository;
	private readonly IBaseRepository<Topic> _topicRepository;
	private readonly IBaseRepository<TopicNote> _topicNoteRepository;
	

	public StatisticsService(IBaseRepository<DailyNote> dailyNoteRepository,
							 IBaseRepository<Subject> subjectRepository,
							 IBaseRepository<Topic> topicRepository,
							 IBaseRepository<TopicNote> topicNoteRepository)
	{
		_dailyNoteRepository = dailyNoteRepository;
		_subjectRepository = subjectRepository;
		_topicRepository = topicRepository;
		_topicNoteRepository = topicNoteRepository;
	}


	public List<string> GetLatesSubjects()
	{
		List<TopicNote> topicNotes = new();
		topicNotes = _topicNoteRepository.GetItems()
											.OrderByDescending(x => x.Date)
											.DistinctBy(x => x.TopicId).ToList();
		List<string> LatestSubjectsName = new();
		int i = 0;

		while (LatestSubjectsName.Count < 3)
		{
			string subjectName = string.Empty;
			int topicId = new();

			topicId = _topicRepository.GetItem(topicNotes[i].TopicId).SubjectId;
			subjectName = _subjectRepository.GetItem(topicId).Name;
			if (!LatestSubjectsName.Contains(subjectName))
			{
				LatestSubjectsName.Add(subjectName);
			}
			i++;
		}

		return LatestSubjectsName;
	}

	public List<bool> GetLast7DaysActivity()
	{
		List<bool> last7DaysActivity = new();
		List<DailyNote> dailyNotes = _dailyNoteRepository.GetItems().Where(x => x.Date >= DateTime.Now.AddDays(-7) && x.Date <= DateTime.Now).ToList(); ;

		Dictionary<string, bool> dailyNotesDict = new()
		{
			{ DateTime.Now.AddDays(-6).ToString("dd.MM.yyyy"), false },
			{ DateTime.Now.AddDays(-5).ToString("dd.MM.yyyy"), false },
			{ DateTime.Now.AddDays(-4).ToString("dd.MM.yyyy"), false },
			{ DateTime.Now.AddDays(-3).ToString("dd.MM.yyyy"), false },
			{ DateTime.Now.AddDays(-2).ToString("dd.MM.yyyy"), false },
			{ DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), false },
			{ DateTime.Now.ToString("dd.MM.yyyy"), false }
		};

		foreach (var dailyNote in dailyNotes)
		{
			string dailyNoteDate = dailyNote.Date.ToString("dd.MM.yyyy");
			if (dailyNotesDict.ContainsKey(dailyNoteDate))
			{
				dailyNotesDict[dailyNoteDate] = true;
			}
		}

		foreach (var dailyStatus in dailyNotesDict.Values)
		{
			last7DaysActivity.Add(dailyStatus);
		}

		return last7DaysActivity;
	}

}
