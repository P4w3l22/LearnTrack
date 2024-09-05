using LearnTrack.MVVM.Models;
using System.Windows.Input;

namespace LearnTrack.MVVM.ViewModels;

public class LearnTrackViewModel
{
    public List<Subject> Subjects { get; set; }
    public Subject CurrentSubject { get; set; }

    public ICommand UpsertCommand =>
        new Command(() => UpsertSubject());
    public ICommand DeleteCommand =>
        new Command(() => DeleteSubject());

    public LearnTrackViewModel()
    {

        SetSubjects();
        SetCurrentSubject();

	}

    private void SetSubjects()
    {
		Subjects = App.SubjectRepository.GetItemsWithChildren();
        if (Subjects is null)
        {
			Subjects = new();
        }
        SetTopicNotes();
    }

    private void SetTopicNotes()
    {
        var topicNotes = App.TopicNoteRepository.GetItems();

        foreach (Subject subject in Subjects)
        {
            foreach (Topic topic in subject.Topics)
            {
                topic.TopicNotes = App.TopicNoteRepository.GetItems(x => x.TopicId == topic.Id);
            }
        }
	}

    private void GetTopicAndTopicNotes()
    {
		var topicNotes = App.TopicNoteRepository.GetItems();
		var topics = App.TopicRepository.GetItems();
	}

    private void DeleteTopicAndTopicNotes()
    {
		var topicNotes = App.TopicNoteRepository.GetItems();
		var topics = App.TopicRepository.GetItems();
		foreach (var topicNote in topicNotes)
        {
            App.TopicNoteRepository.DeleteItem(topicNote);
        }
        foreach (var topic in topics)
        {
            App.TopicRepository.DeleteItem(topic);
        }
        CurrentSubject = Subjects[0];
        DeleteSubject();
    }
	

    private void SetCurrentSubject()
    {
        if (Subjects.Count == 0 | Subjects is null)
        {
            Subject subject = new() { Name = "C#" };
            Topic topic1 = new()
            {
                Name = "Metody asynchroniczne",
                TopicNotes = new List<TopicNote>()
                {
                    new TopicNote() { Description = "Pozwalają na wykonywanie akcji równolegle do czasu działania programu", Date = DateTime.Now },
                    new TopicNote() { Description = "Realizuje się poprzez dodanie słowa async do definicji metody", Date = DateTime.Now },
                    new TopicNote() { Description = "Metody asynchroniczne wywołuje się poprzez dodanie słowa await przed jej nazwą", Date = DateTime.Now }
                }
            };
            Topic topic2 = new()
            {
                Name = "Pytania rekrutacyjne",
                TopicNotes = new List<TopicNote>()
                {
                    new TopicNote() { Description = "Czym się różni klasa abstrakcyjna od interfejsu?", Date = DateTime.Now },
                    new TopicNote() { Description = "Podaj dostępne zakresy widoczności", Date = DateTime.Now }
                }
            };

		    subject.Topics = new List<Topic>
		    {
			    topic1, 
                topic2
		    };

			CurrentSubject = subject;
        }

		CurrentSubject = Subjects[0];

    }

    private void UpsertSubject()
    {
        App.SubjectRepository.UpsertItemWithChildren(CurrentSubject);
    }

	private void DeleteSubject()
    {
        App.SubjectRepository.DeleteItem(CurrentSubject);
    }

}
