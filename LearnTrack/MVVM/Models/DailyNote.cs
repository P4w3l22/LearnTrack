using LearnTrack.MVVM.Models.Base;
using SQLite;

namespace LearnTrack.MVVM.Models;

public class DailyNote : TableData
{
    [NotNull]
    public string Title { get; set; }
    [NotNull]
	public string Content { get; set; }
    [NotNull]
	public DateTime Date { get; set; }
    [NotNull]
	public bool IsCompleted { get; set; } = false;
}
