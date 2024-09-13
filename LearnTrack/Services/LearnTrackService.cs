using LearnTrack.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnTrack.Services;

public class LearnTrackService : LearnTrackBaseService
{
	public TopicNote CurrentTopicNote { get; set; } = new()
	{
		Date = DateTime.Now
	};
	public Subject NewSubject { get; set; }
	public Topic? NewTopic { get; set; }

    public LearnTrackService()
    {
        
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
		CurrentTopicNote = new() { Date = DateTime.Now };
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

}
