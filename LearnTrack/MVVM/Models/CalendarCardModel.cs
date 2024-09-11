namespace LearnTrack.MVVM.Models;

public class CalendarCardModel
{
    public int DayNumber { get; set; }
    public string TasksStatus { get; set; }
    public int ColumnId { get; set; }
    public int RowId { get; set; }
    public bool IsCurrentMonth { get; set; }
}
