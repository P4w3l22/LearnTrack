using LearnTrack.MVVM.Models.Base;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace LearnTrack.MVVM.Models;

[Table("TopicNotes")]
public class TopicNote : TableData
{
    [NotNull]
    public string Description { get; set; }
    [NotNull]
    public DateTime Date { get; set; }

	[ForeignKey(typeof(Topic))]
	public int TopicId { get; set; }
}
