namespace LearnTrack.MVVM.Models;

public class TopicModel
{
    public int SubjectId { get; set; }
    public string Topic { get; set; }
    public List<ShortNote> Notes { get; set; }
}
