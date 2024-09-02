namespace LearnTrack.Models;

public class TopicNote
{
	public int Id { get; set; }
	public int TopicId { get; set; }
	public string Description { get; set; }
	public DateTime Date {  get; set; }
}
