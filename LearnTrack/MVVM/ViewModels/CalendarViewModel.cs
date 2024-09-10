using LearnTrack.MVVM.Models;
using PropertyChanged;
using System.Windows.Input;
using System.Xml.Serialization;

namespace LearnTrack.MVVM.ViewModels;

[AddINotifyPropertyChangedInterface]
class CalendarViewModel
{
	public List<CalendarCardModel> CalendarCards { get; set; }

	public int DaysInSelectedMonth
    {
        get =>
            DateTime.DaysInMonth(SelectedYear, SelectedMonth);
    }
    public int SelectedDay { get; set; }
    public int SelectedMonth { get; set; }
    public int SelectedMonthId { get; set; }
    public int SelectedYear { get; set; }
    public string SelectedDate { get; set; }
    public List<DailyNote> CurrentDailyNotes { get; set; }

    public ICommand ChangeSelectedDayCommand =>
        new Command<object>((day) => ChangeSelectedDay(day));
    public ICommand IncrementSelectedMonthCommand =>
        new Command(() => IncrementSelectedMonth());
	public ICommand DecrementSelectedMonthCommand =>
		new Command(() => DecrementSelectedMonth());



	public CalendarViewModel()
    {
        SetDefaultCurrentDate();
        SetCalendarCards();
    }

    private void SetDefaultCurrentDate()
    {
		SelectedDate = DateTime.Now.ToString("dd.MM.yyyy");
		SelectedDay = DateTime.Now.Day;
		SelectedMonth = DateTime.Now.Month;
        SelectedMonthId = SelectedMonth - 1;
		SelectedYear = DateTime.Now.Year;
    }

    private void ChangeSelectedDay(object day)
	{
        SelectedDay = (int)day;
        SelectedDate = new DateTime(SelectedYear, SelectedMonth, SelectedDay).ToString("dd.MM.yyyy");
    }

    private void IncrementSelectedMonth()
	{
        if (SelectedMonth == 12)
        {
            SelectedMonth = 1;
            SelectedYear++;
        }
        else
        {
            SelectedMonth++;
        }
		SelectedMonthId = SelectedMonth - 1;
		SetCalendarCards();
	}

	private void DecrementSelectedMonth()
	{
        if (SelectedMonth == 1)
        {
            SelectedMonth = 12;
            SelectedYear--;
        }
        else
        {
            SelectedMonth--;
        }
		SelectedMonthId = SelectedMonth - 1;
		SetCalendarCards();
	}

	private void SetCalendarCards()
	{
		int daysInMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);
        int daysInPrevMonth;

		if (SelectedMonth == 1)
        {
			daysInPrevMonth = DateTime.DaysInMonth(SelectedYear - 1, 12);
		}
        else
        {
            daysInPrevMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth - 1);
        }
        
		Random random = new();
        CalendarCards = new();

        int dayOfWeekAt1 = (int)new DateTime(SelectedYear, SelectedMonth, 1).DayOfWeek;
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
				NotesNumber = random.Next(0, 4),
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
				NotesNumber = random.Next(0, 4),
				ColumnId = (i - 1 + dayOfWeekAt1) % 7,
				RowId = (i - 1 + dayOfWeekAt1) / 7,
				IsCurrentMonth = true
			});
		}
	}
}
