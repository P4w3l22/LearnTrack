using LearnTrack.MVVM.Models;
using Newtonsoft.Json;

namespace LearnTrack.MVVM.ViewModels;

public class MainPageViewModel
{

    public List<string> LatestSubjectsName { get; set; }
    public List<bool> Last7DaysActivity { get; set; }


	public MainPageViewModel()
    {
		SetLatestSubjects();
        SetLast7DaysActivity();

	}

    // TODO: dodac dedykowany serwis do pobierania danych statystycznych, w tym tych z tego viewModel

    private void SetLast7DaysActivity()
    {
        Last7DaysActivity = new();
        List<DailyNote> dailyNotes = App.DailyNoteRepository.GetItems().Where(x => x.Date >= DateTime.Now.AddDays(-7) && x.Date <= DateTime.Now).ToList();;
        
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
            Last7DaysActivity.Add(dailyStatus);
        }
	}

    private async void SetLatestSubjects()
    {
        List<TopicNote> topicNotes = new();
		topicNotes = App.TopicNoteRepository.GetItems().OrderByDescending(x => x.Date).DistinctBy(x => x.TopicId).ToList();
		LatestSubjectsName = new();
        int i = 0;

        while (LatestSubjectsName.Count < 3)
        {
            string subjectName = string.Empty;
            int topicId = new();

			topicId = App.TopicRepository.GetItem(topicNotes[i].TopicId).SubjectId;
            subjectName = App.SubjectRepository.GetItem(topicId).Name;
            if (!LatestSubjectsName.Contains(subjectName))
            {
				LatestSubjectsName.Add(subjectName);
            }
            i++;
        }
    }

}
