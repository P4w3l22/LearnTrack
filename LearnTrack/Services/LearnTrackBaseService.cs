using LearnTrack.MVVM.Models;

namespace LearnTrack.Services;

public class LearnTrackBaseService
{
	public List<Subject> Subjects { get; set; }
	public Subject CurrentSubject { get; set; }
	public Topic CurrentTopic { get; set; }

    public LearnTrackBaseService()
    {
        
    }

	public void Refresh(bool defaultValues = false, bool isCreating = false)
	{
		SetSubjects();
		SetTopicNotes();

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

	private void SetSubjects()
	{
		Subjects = App.SubjectRepository.GetItemsWithChildren();
		if (Subjects is null)
		{
			Subjects = new();
		}
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

	internal void SetCurrentSubject(object id) =>
		CurrentSubject = Subjects.FirstOrDefault(x => x.Id == (int)id);

	internal void ChangeCurrentTopic(object id) =>
		CurrentTopic = CurrentSubject.Topics.FirstOrDefault(x => x.Id == (int)id);
	

}
