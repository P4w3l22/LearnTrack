using LearnTrack.MVVM.Models;

namespace LearnTrack.Services;

public class CalendarService : ICalendarService
{
	private List<CalendarCardModel> CalendarCards { get; set; }
	private Dictionary<string, int[]> DailyNotesStatusPerDay { get; set; }

	public CalendarService()
	{
	}

	public List<DailyNote> GetDailyNotes(string selectedDate) =>
		App.DailyNoteRepository.GetItems().Where(x => x.Date.ToString("dd.MM.yyyy") == selectedDate).ToList();

	public void UpsertDailyNote(DailyNote dailyNote)
	{
		App.DailyNoteRepository.UpsertItemWithChildren(dailyNote);
	}

	public void DeleteDailyNote(int id, List<DailyNote> currentDailyNotes)
	{
		App.DailyNoteRepository.DeleteItem(currentDailyNotes.FirstOrDefault(x => x.Id == (int)id));
	}


	public List<CalendarCardModel> GetCalendarCards(int selectedYear, int selectedMonth)
	{
		SetTasksStatusPerDate();

		int daysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonth);
		int daysInPrevMonth;

		if (selectedMonth == 1)
		{
			daysInPrevMonth = DateTime.DaysInMonth(selectedYear - 1, 12);
		}
		else
		{
			daysInPrevMonth = DateTime.DaysInMonth(selectedYear, selectedMonth - 1);
		}

		Random random = new();
		CalendarCards = new();

		int dayOfWeekAt1 = (int)new DateTime(selectedYear, selectedMonth, 1).DayOfWeek;
		if (dayOfWeekAt1 == 0)
		{
			dayOfWeekAt1 = 6;
		}
		else
		{
			dayOfWeekAt1--;
		}

		for (int i = 1; i <= dayOfWeekAt1; i++)
		{
			CalendarCards.Add(new CalendarCardModel()
			{
				DayNumber = i + daysInPrevMonth - dayOfWeekAt1,
				ColumnId = (i - 1) % 7,
				RowId = (i - 1) / 7,
				IsCurrentMonth = false
			});
		}

		for (int i = 1; i <= daysInMonth; i++)
		{
			CalendarCards.Add(new CalendarCardModel()
			{
				DayNumber = i,
				TasksStatus = GetNoteNumber(selectedYear, selectedMonth, i),
				ColumnId = (i - 1 + dayOfWeekAt1) % 7,
				RowId = (i - 1 + dayOfWeekAt1) / 7,
				IsCurrentMonth = true
			});
		}
		return CalendarCards;
	}

	private void SetTasksStatusPerDate()
	{
		List<DailyNote> notes = new();
		notes = App.DailyNoteRepository.GetItems();
		DailyNotesStatusPerDay = new();
		foreach (DailyNote note in notes)
		{
			string noteDate = note.Date.ToString("dd.MM.yyyy");

			if (DailyNotesStatusPerDay.ContainsKey(noteDate))
			{
				DailyNotesStatusPerDay[noteDate][1]++;
				if (note.IsCompleted)
				{
					DailyNotesStatusPerDay[noteDate][0]++;
				}
			}
			else
			{
				int[] dailyTasksStatus = { (note.IsCompleted ? 1 : 0), 1 };

				DailyNotesStatusPerDay.Add(noteDate, dailyTasksStatus);
			}
		}
	}

	private string GetNoteNumber(int year, int month, int day)
	{
		string currentDate = new DateTime(year, month, day).ToString("dd.MM.yyyy");
		if (DailyNotesStatusPerDay.ContainsKey(currentDate))
		{
			//return $"{DailyNotesStatusPerDay[currentDate][0]} / {DailyNotesStatusPerDay[currentDate][1]}";
			return $"{DailyNotesStatusPerDay[currentDate][1]}";
		}
		return "0";
	}

}
