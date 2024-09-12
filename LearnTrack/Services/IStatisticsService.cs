
namespace LearnTrack.Services;

public interface IStatisticsService
{
	List<bool> GetLast7DaysActivity();
	List<string> GetLatesSubjects();
}