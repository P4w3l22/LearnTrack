﻿using LearnTrack.MVVM.Models;
using PropertyChanged;
using System.Windows.Input;

namespace LearnTrack.MVVM.ViewModels;

[AddINotifyPropertyChangedInterface]
public class LearnTrackViewModel
{
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

    public List<Subject> Subjects { get; set; }
    public Subject? CurrentSubject { get; set; }
	public Topic? CurrentTopic { get; set; }

    public ICommand UpsertCommand =>
        new Command(() => SaveCurrentSubject());
    public ICommand DeleteCommand =>
        new Command(() => DeleteCurrentSubject());
	public ICommand ChangeSubjectCommand =>
		new Command<object>((id) => ChangeCurrentSubject(id));
	public ICommand ChangeTopicCommand =>
		new Command<object>((id) => SetCurrentTopic(id));

    public LearnTrackViewModel()
    {
        SetSubjects();
        SetCurrentSubject(Subjects[0].Id);
        SetCurrentTopic(CurrentSubject.Topics[0].Id);
	}

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

    private void Refresh()
    {
        SetSubjects();
        SetCurrentSubject(CurrentSubject.Id);
        SetCurrentTopic(CurrentTopic.Id);
    }

    private void ChangeCurrentSubject(object id)
    {
		SetCurrentSubject(id);
		SetCurrentTopic(CurrentSubject.Topics[0].Id);
	}

	private void SetCurrentSubject(object id)
	{
		CurrentSubject = Subjects.FirstOrDefault(x => x.Id == (int)id);
	}

	private void SetCurrentTopic(object id)
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

    private void SaveCurrentSubject()
    {
        App.SubjectRepository.UpsertItemWithChildren(CurrentSubject);
    }

    private void SaveCurrentTopicNote()
    {
        App.TopicNoteRepository.UpsertItemWithChildren(CurrentTopicNote);
    }

	private void DeleteCurrentSubject()
    {
        App.SubjectRepository.DeleteItem(CurrentSubject);
    }

}
