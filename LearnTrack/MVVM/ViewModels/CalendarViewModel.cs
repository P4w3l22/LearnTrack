using LearnTrack.MVVM.Models;
using LearnTrack.Services;
using Newtonsoft.Json.Bson;
using PropertyChanged;
using System.Windows.Input;
using System.Xml.Serialization;

namespace LearnTrack.MVVM.ViewModels;

[AddINotifyPropertyChangedInterface]
public class CalendarViewModel
{
	public List<CalendarCardModel> CalendarCards { get; set; }
    public List<string> MonthNames = new()
    {
        "Styczeń",
        "Luty",
        "Marzec",
        "Kwiecień",
        "Maj",
        "Czerwiec",
        "Lipiec",
        "Sierpień",
        "Wrzesień",
        "Październik",
        "Listopad",
        "Grudzień"
    };
    public string SelectedMonthName {  get; set; }

    public int SelectedDay { get; set; }
    public int SelectedMonth { get; set; }
    public int SelectedYear { get; set; }
    public string SelectedDate { get; set; }
    public DailyNote NewDailyNote { get; set; }
    public List<DailyNote> CurrentDailyNotes { get; set; }

    public ICommand ChangeSelectedDayCommand =>
        new Command<object>((day) => ChangeSelectedDay(day));
    public ICommand IncrementSelectedMonthCommand =>
        new Command(() => IncrementSelectedMonth());
	public ICommand DecrementSelectedMonthCommand =>
		new Command(() => DecrementSelectedMonth());
    public ICommand UpsertDailyNoteCommand =>
        new Command(() => InsertDailyNote());
	public ICommand DeleteDailyNoteCommand =>
	new Command<object>((id) => DeleteDailyNote(id));

    private readonly ICalendarService _calendarService;


	public CalendarViewModel(ICalendarService calendarService)
    {
        _calendarService = calendarService;

        SetDefaultCurrentDate();
        SetCalendarCards();
        SetCurrentDailyNotes();
        SetSelectedMonthName();
		
	}

    private void SetDefaultCurrentDate()
    {
		SelectedDate = DateTime.Now.ToString("dd.MM.yyyy");
		SelectedDay = DateTime.Now.Day;
		SelectedMonth = DateTime.Now.Month;
		SelectedYear = DateTime.Now.Year;
    }

	private void SetCalendarCards()
	{
        CalendarCards = _calendarService.GetCalendarCards(SelectedYear, SelectedMonth);
	}
	private void SetCurrentDailyNotes()
    {
        NewDailyNote = new() 
        { 
            Date = new DateTime(SelectedYear, SelectedMonth, SelectedDay) 
        };
        
        CurrentDailyNotes = _calendarService.GetDailyNotes(SelectedDate);
    }

    private void SetSelectedMonthName()
    {
		SelectedMonthName = MonthNames[SelectedMonth - 1];
	}

    private void ChangeSelectedDay(object day)
	{
        SelectedDay = (int)day;
        SelectedDate = new DateTime(SelectedYear, SelectedMonth, SelectedDay).ToString("dd.MM.yyyy");
		SetCurrentDailyNotes();
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
		SetCalendarCards();
		SetSelectedMonthName();
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
		SetCalendarCards();
		SetSelectedMonthName();
	}

    private void InsertDailyNote()
    {
        App.DailyNoteRepository.UpsertItemWithChildren(NewDailyNote);
        SetCurrentDailyNotes();
	}

	public void UpdateDailyNotes()
    {
        foreach (DailyNote dailyNote in CurrentDailyNotes)
        {
			_calendarService.UpsertDailyNote(dailyNote);
		}
        //SetTasksStatusPerDate(SelectedDate);
        //SetCalendarCards();
	}

    private void DeleteDailyNote(object id)
    {
        _calendarService.DeleteDailyNote((int)id, CurrentDailyNotes);
		SetCurrentDailyNotes();
	}

}
