using LearnTrack.MVVM.Models.Base;
using SQLite;

namespace LearnTrack.MVVM.Models;

[Table("TopicNotes")]
public class TopicNote : TableData
{
    [NotNull]
    public int TopicId { get; set; }
    [NotNull]
    public string Description { get; set; }
    [NotNull]
    public DateTime Date { get; set; }
}
