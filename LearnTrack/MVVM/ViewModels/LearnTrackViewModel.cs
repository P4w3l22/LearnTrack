using LearnTrack.MVVM.Models;
using PropertyChanged;
using System.Windows.Input;

namespace LearnTrack.MVVM.ViewModels;

[AddINotifyPropertyChangedInterface]
public class LearnTrackViewModel
{
    // TopicNote CRUD variables + commands
    public TopicNote CurrentTopicNote { get; set; } = new()
    {
        Description = "",
        Date = DateTime.Now
    };
    public ICommand UpsertTopicNoteCommand =>
        new Command(() => UpsertTopicNote());
    public ICommand DeleteTopicNoteCommand =>
        new Command<object>((id) => DeleteTopicNote(id));
    public ICommand ChangeTopicNoteCommand =>
        new Command<object>((id) => ChangeCurrentTopicNote(id));


	// Topic CRUD variables + commands
	public Topic? CurrentTopic { get; set; }
    public Topic? NewTopic { get; set; }
    public ICommand UpsertTopicCommand =>
        new Command(() => UpsertTopic());
	public ICommand SetUpdateTopicCommand =>
		new Command(() => SetUpdateTopic());
	public ICommand DeleteTopicCommand =>
        new Command(() => DeleteTopic());
	public ICommand ChangeTopicCommand =>
		new Command<object>((id) => SetCurrentTopic(id));


	// Subject CRUD variables + commands
	public Subject NewSubject { get; set; }
    public ICommand UpsertSubjectCommand =>
        new Command(() => UpsertSubject());



	// LearnTrackPage details variables + commands
	public List<Subject> Subjects { get; set; }
    public Subject? CurrentSubject { get; set; }
    //public ICommand UpsertCommand =>
    //    new Command(() => SaveCurrentSubject());
    public ICommand DeleteSubjectCommand =>
        new Command(() => DeleteCurrentSubject());
	public ICommand ChangeSubjectCommand =>
		new Command<object>((id) => ChangeCurrentSubject(id));


    
    public LearnTrackViewModel()
    {
        SetSubjects();
        SetCurrentSubject(Subjects[0].Id);
        SetCurrentTopic(CurrentSubject.Topics[0].Id);
        NewTopic = new();
        NewSubject = new();
	}


	// TopicNote CRUD methods
	private void ChangeCurrentTopicNote(object id)
    {
        CurrentTopicNote = CurrentTopic.TopicNotes.FirstOrDefault(x => x.Id == (int)id);
    }

	private void UpsertTopicNote()
    {
        if (CurrentTopicNote.TopicId == 0)
        {
            CurrentTopicNote.TopicId = CurrentTopic.Id; 
        }
        SaveCurrentTopicNote();
        Refresh();
        CurrentTopicNote = new() { Date=DateTime.Now };
	}

	private void GetTopicNotes()
    {
        var topicNotes = App.TopicNoteRepository.GetItems();
    }

    private void DeleteTopicNote(object id)
    {
        TopicNote topicNote = CurrentTopic.TopicNotes.FirstOrDefault(x => x.Id == (int)id);
        App.TopicNoteRepository.DeleteItem(topicNote);
		Refresh();
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

    private void SaveCurrentTopicNote()
    {
        App.TopicNoteRepository.UpsertItemWithChildren(CurrentTopicNote);
    }


	// Topic CRUD methods
	private void SetCurrentTopic(object id)
	{
		CurrentTopic = CurrentSubject.Topics.FirstOrDefault(x => x.Id == (int)id);
	}

    private void UpsertTopic()
    {
        if (NewTopic.SubjectId == 0)
        {
            NewTopic.SubjectId = CurrentSubject.Id;
        }
		SaveModelTopic();
        Refresh();
		NewTopic = new();
	}

    private void SetUpdateTopic()
    {
        NewTopic = CurrentTopic;
    }

	private void DeleteTopic()
    {
        App.TopicRepository.DeleteItem(CurrentTopic);
        Refresh();
        SetCurrentTopic(CurrentSubject.Topics[CurrentSubject.Topics.Count() - 1].Id);
    }

	private void SaveModelTopic()
    {
        App.TopicRepository.UpsertItemWithChildren(NewTopic);
    }


    // Subject CRUD methods
    private void UpsertSubject()
    {
        if (NewSubject.Name is null ||  NewSubject.Name.Length == 0)
        {
            NewSubject = CurrentSubject;
        }
        SaveModelSubject();
        Refresh();
        NewSubject = new();
    }

    private void DeleteSubject()
    {

    }

    private void SaveModelSubject()
    {
        App.SubjectRepository.UpsertItemWithChildren(NewSubject);
    }


	// LearnTrackPage details methods
	private void Refresh()
    {
        SetSubjects();
        SetCurrentSubject(CurrentSubject.Id);
        SetCurrentTopic(CurrentTopic.Id);
    }
    private void ChangeCurrentSubject(object id)
    {
		SetCurrentSubject(id);
        if (CurrentSubject.Topics.Count > 0)
        {
            SetCurrentTopic(CurrentSubject.Topics[0].Id);
        }
        else
        {
            CurrentTopic = new()
            {
                Name = "Brak tematów"
            };
        }
		
	}
	private void SetCurrentSubject(object id)
	{
		CurrentSubject = Subjects.FirstOrDefault(x => x.Id == (int)id);
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
    private void SaveCurrentSubject()
    {
        App.SubjectRepository.UpsertItemWithChildren(CurrentSubject);
    }
	private void DeleteCurrentSubject()
    {
        App.SubjectRepository.DeleteItem(CurrentSubject);
		SetSubjects();
		SetCurrentSubject(Subjects[0].Id);
		SetCurrentTopic(CurrentSubject.Topics[0].Id);
	}

}
