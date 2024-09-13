using LearnTrack.MVVM.Models;
using PropertyChanged;
using System.Windows.Input;

namespace LearnTrack.MVVM.ViewModels;

[AddINotifyPropertyChangedInterface]
public class LearnTrackViewModel
{
	public List<Subject> Subjects { get; set; }
    public Subject? CurrentSubject { get; set; }
	public Topic? CurrentTopic { get; set; }
    public TopicNote CurrentTopicNote { get; set; } = new()
    {
        Date = DateTime.Now
    };
	public Subject NewSubject { get; set; }
    public Topic? NewTopic { get; set; }

    public ICommand ChangeSubjectCommand =>
		new Command<object>((id) => ChangeCurrentSubject(id));
	public ICommand ChangeTopicCommand =>
		new Command<object>((id) => ChangeCurrentTopic(id));
    public ICommand ChangeTopicNoteCommand =>
        new Command<object>((id) => ChangeCurrentTopicNote(id));
    public ICommand UpsertSubjectCommand =>
        new Command(() => UpsertSubject());
    public ICommand UpsertTopicCommand =>
        new Command(() => UpsertTopic());
    public ICommand UpsertTopicNoteCommand =>
        new Command(() => UpsertTopicNote());
    public ICommand DeleteSubjectCommand =>
        new Command(() => DeleteSubject());
	public ICommand DeleteTopicCommand =>
        new Command(() => DeleteTopic());
    public ICommand DeleteTopicNoteCommand =>
        new Command<object>((id) => DeleteTopicNote(id));
	public ICommand SetUpdateTopicCommand =>
		new Command(() => SetUpdateTopic());

    
    public LearnTrackViewModel()
    {
        // zamienić na refresh
            SetSubjects();
            SetCurrentSubject(Subjects[0].Id);
		    ChangeCurrentTopic(CurrentSubject.Topics[0].Id);
        NewTopic = new();
        NewSubject = new();
	}


    private void ChangeCurrentSubject(object id)
    {
		SetCurrentSubject(id);
        if (CurrentSubject.Topics.Count > 0)
        {
			ChangeCurrentTopic(CurrentSubject.Topics[0].Id);
        }
        else
        {
            CurrentTopic = new()
            {
                Name = ""
            };
        }
	}

	private void ChangeCurrentTopicNote(object id) =>
        CurrentTopicNote = CurrentTopic.TopicNotes.FirstOrDefault(x => x.Id == (int)id);
    
	private void UpsertSubject()
	{
		if (NewSubject.Name is null || NewSubject.Name.Length == 0)
		{
			NewSubject = CurrentSubject;
		}
		SaveModelSubject();
		Refresh(true, true);
        if (CurrentTopic.TopicNotes.Count == 0)
        {
            CurrentTopic = new() { Name = "" };
        }
		NewSubject = new();
	}

	private void UpsertTopic()
	{
		if (NewTopic.SubjectId == 0)
		{
			NewTopic.SubjectId = CurrentSubject.Id;
		}
		SaveModelTopic();
		Refresh();
        if (CurrentSubject.Topics.Count == 0)
        {
            CurrentSubject = Subjects.FirstOrDefault(x => x.Id == CurrentSubject.Id);
        }
        CurrentTopic = CurrentSubject.Topics[CurrentSubject.Topics.Count - 1];
		NewTopic = new();
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

	private void DeleteSubject()
	{
		App.SubjectRepository.DeleteItem(CurrentSubject);
        Refresh(true);
		SetCurrentSubject(Subjects[Subjects.Count - 1].Id);
        if (CurrentSubject.Topics.Count > 0)
        {
			ChangeCurrentTopic(CurrentSubject.Topics[0].Id);
		}
        else
        {
            CurrentTopic = new() { Name = "" };
        }
	}

	private void DeleteTopic()
	{
		App.TopicRepository.DeleteItem(CurrentTopic);
		Refresh();
        if (CurrentSubject.Topics.Count() > 0)
        {
			ChangeCurrentTopic(CurrentSubject.Topics[CurrentSubject.Topics.Count() - 1].Id);
        }
        else
        {
            CurrentTopic = new() { Name = "" };
        }
		
	}

    private void DeleteTopicNote(object id)
    {
        TopicNote topicNote = CurrentTopic.TopicNotes.FirstOrDefault(x => x.Id == (int)id);
        App.TopicNoteRepository.DeleteItem(topicNote);
		Refresh();
	}
    
    private void SetUpdateTopic()
    {
        NewTopic = CurrentTopic;
    }

    private void SaveCurrentTopicNote()
    {
        App.TopicNoteRepository.UpsertItemWithChildren(CurrentTopicNote);
    }

	private void SaveModelTopic()
    {
        App.TopicRepository.UpsertItemWithChildren(NewTopic);
    }

    private void SaveModelSubject()
    {
        App.SubjectRepository.UpsertItemWithChildren(NewSubject);
    }
    

    // Wyodrębnić do serwisu
    // TODO: refaktoryzacja refresh
	private void Refresh(bool defaultValues = false, bool isCreating = false)
    {
        SetSubjects();
        if (CurrentSubject.Topics.Count > 0 | isCreating)
        {
            if (defaultValues)
            {
			    SetCurrentSubject(Subjects[0].Id);
				ChangeCurrentTopic(CurrentSubject.Topics[0].Id);
		    }
            else
            {
                SetCurrentSubject(CurrentSubject.Id);
				ChangeCurrentTopic(CurrentTopic.Id);
            }
        }
        else
        {
            CurrentTopic = new() { Name = "" };
        }
    }
    
	private void SetCurrentSubject(object id) => 
        CurrentSubject = Subjects.FirstOrDefault(x => x.Id == (int)id);

    private void ChangeCurrentTopic(object id)
	{
		CurrentTopic = CurrentSubject.Topics.FirstOrDefault(x => x.Id == (int)id);
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
}
