using LearnTrack.MVVM.Models.Base;
using SQLite;

namespace LearnTrack.MVVM.Models;

[Table("Topics")]
public class Topic : TableData
{
    [NotNull]
    public int SubjectId { get; set; }
    [NotNull]
    public string Name { get; set; }
}
