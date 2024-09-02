using LearnTrack.Models;

namespace LearnTrack.ViewModels;

public class LearnTrackViewModel
{
	public List<TopicModel> TopicList { get; set; }

    public LearnTrackViewModel()
    {
        SetTopicList();
    }

    private void SetTopicList()
    {
        TopicList = new List<TopicModel>()
        {
            new TopicModel()
            {
                SubjectId=0,
                Topic="Metody asynchroniczne",
                Notes=new List<ShortNote>()
                {
                    new ShortNote() { Description="Pozwalają na wykonywanie akcji równolegle do czasu działania programu", CreatedDate=DateTime.Now },
                    new ShortNote() { Description="Realizuje się poprzez dodanie słowa async do definicji metody", CreatedDate=DateTime.Now },
                    new ShortNote() { Description="Metody asynchroniczne wywołuje się poprzez dodanie słowa await przed jej nazwą", CreatedDate=DateTime.Now }
                }
            },
			new TopicModel()
			{
                SubjectId=0,
				Topic="Pytania rekrutacyjne",
				Notes=new List<ShortNote>()
				{
					new ShortNote() { Description="Czym się różni klasa abstrakcyjna od interfejsu?", CreatedDate=DateTime.Now },
					new ShortNote() { Description="Podaj dostępne zakresy widoczności", CreatedDate=DateTime.Now }
				}
			}
		};
    }
}
