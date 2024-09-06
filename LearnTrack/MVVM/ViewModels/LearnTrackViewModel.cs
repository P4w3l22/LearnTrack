﻿using LearnTrack.MVVM.Models;
using PropertyChanged;
using System.Windows.Input;

namespace LearnTrack.MVVM.ViewModels;

[AddINotifyPropertyChangedInterface]
public class LearnTrackViewModel
{
    public List<Subject> Subjects { get; set; }
    public Subject? CurrentSubject { get; set; }
	public Topic? CurrentTopic { get; set; }

    public ICommand UpsertCommand =>
        new Command(() => UpsertSubject());
    public ICommand DeleteCommand =>
        new Command(() => DeleteSubject());
	public ICommand ChangeSubjectCommand =>
		new Command<object>((id) => SetCurrentSubject(id));
	public ICommand ChangeTopicCommand =>
		new Command<object>((id) => SetCurrentTopic(id));

    public LearnTrackViewModel()
    {

        SetSubjects();
        SetCurrentSubject(Subjects[0].Id);
		SetCurrentTopic(CurrentSubject.Topics[0].Id);

	}

	private void SetCurrentSubject(object id)
	{
		CurrentSubject = Subjects.FirstOrDefault(x => x.Id == (int)id);
		SetCurrentTopic(CurrentSubject.Topics[0].Id);
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

	private void SetCurrentTopic(int topicId)
	{
		CurrentTopic = CurrentSubject.Topics.FirstOrDefault(x => x.Id == topicId);
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
