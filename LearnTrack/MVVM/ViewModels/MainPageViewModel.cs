using LearnTrack.Services;

namespace LearnTrack.MVVM.ViewModels;

public class MainPageViewModel
{

    public List<string> LatestSubjectsName { get; set; }
    public List<bool> Last7DaysActivity { get; set; }

	private readonly IStatisticsService _statisticsService;


	public MainPageViewModel(IStatisticsService statisticsService)
    {
		_statisticsService = statisticsService;
		LatestSubjectsName = _statisticsService.GetLatesSubjects();
        Last7DaysActivity = _statisticsService.GetLast7DaysActivity();
	}

}
