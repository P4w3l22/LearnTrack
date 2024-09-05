using LearnTrack.MVVM.Models.Base;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace LearnTrack.MVVM.Models;

[Table("Topics")]
public class Topic : TableData
{
    [NotNull]
    public string Name { get; set; }

	[ForeignKey(typeof(Subject))]
	public int SubjectId { get; set; }

	[OneToMany(CascadeOperations = CascadeOperation.All)]
	public List<TopicNote> TopicNotes { get; set; }
}
