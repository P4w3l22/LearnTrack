using LearnTrack.MVVM.Models.Base;
using SQLite;

namespace LearnTrack.MVVM.Models;

[Table("Subjects")]
public class Subject : TableData
{
    [NotNull]
    public string Name { get; set; }
}
