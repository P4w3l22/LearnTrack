using LearnTrack.MVVM.Models.Base;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace LearnTrack.MVVM.Models;

[Table("Subjects")]
public class Subject : TableData
{
    [NotNull]
    public string Name { get; set; }

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<Topic> Topics { get; set; }
}
