using SQLite;

namespace LearnTrack.MVVM.Models.Base;

public class TableData
{
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
}
