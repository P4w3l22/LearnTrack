using LearnTrack.MVVM.Models;
using LearnTrack.Services;
using Microcharts;
using SkiaSharp;

namespace LearnTrack.MVVM.ViewModels;

public class MainPageViewModel
{

    public List<string> LatestSubjectsName { get; set; }
    public List<bool> Last7DaysActivity { get; set; }
	public ChartEntry[] Entries { get; set; }

	private readonly IStatisticsService _statisticsService;


	public MainPageViewModel(IStatisticsService statisticsService)
    {
		_statisticsService = statisticsService;
		LatestSubjectsName = _statisticsService.GetLatesSubjects();
        Last7DaysActivity = _statisticsService.GetLast7DaysActivity();

		SetEntries();
	}

	public void SetEntries()
	{
		List<Subject> subjectsTopicsCount = _statisticsService.GetSubjectsTopicsCount();
		Entries = new ChartEntry[subjectsTopicsCount.Count];
		int i = 0;

		foreach (Subject subject in subjectsTopicsCount)
		{
			Entries[i] = new ChartEntry(subject.Topics.Count)
			{
				Label = subject.Name,
				ValueLabel = subject.Topics.Count.ToString(),
				ValueLabelColor = SKColor.Parse("#ffffff"),
				Color = SKColor.Parse("#8F00FF")
			};
			i++;
		}
	}

}
