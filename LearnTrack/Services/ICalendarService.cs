using LearnTrack.MVVM.Models;

namespace LearnTrack.Services;

public interface ICalendarService
{
	List<DailyNote> GetDailyNotes(string selectedDate);
	void UpsertDailyNote(DailyNote dailyNote);
	void DeleteDailyNote(int id, List<DailyNote> currentDailyNotes);
	List<CalendarCardModel> GetCalendarCards(int selectedYear, int selectedMonth);
}