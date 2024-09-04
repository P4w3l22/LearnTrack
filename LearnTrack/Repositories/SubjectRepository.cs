using LearnTrack.MVVM.Models;
using SQLite;
using System.Linq.Expressions;

namespace LearnTrack.Repositories;

public class SubjectRepository
{
	SQLiteConnection connection;
	public string StatusMessage { get; set; }

    public SubjectRepository()
    {
        connection = new SQLiteConnection(SQLiteConstants.DatabasePath, SQLiteConstants.Flags);
        connection.CreateTable<Subject>();
    }

    public void AddOrUpdate(Subject subject)
    {
        int result = 0;
        try
        {
            if (subject.Id != 0)
            {
                result = connection.Update(subject);
                StatusMessage = $"{result} row(s) updated";
            }
            else
            {
                result = connection.Insert(subject);
                StatusMessage = $"{result} row(s) added";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
    }

	public List<Subject> GetAll()
	{
		try
		{
			return connection.Table<Subject>().ToList();
		}
		catch (Exception ex)
		{
			StatusMessage = $"Error: {ex.Message}";
		}
		return null;
	}

	public List<Subject> GetAll(Expression<Func<Subject, bool>> predicate)
	{
		try
		{
			return connection.Table<Subject>().Where(predicate).ToList();
		}
		catch (Exception ex)
		{
			StatusMessage = $"Error: {ex.Message}";
		}
		return null;
	}

	public Subject Get(int id)
	{
		try
		{
			return connection.Table<Subject>().FirstOrDefault(x => x.Id == id);
		}
		catch (Exception ex)
		{
			StatusMessage = $"Error: {ex.Message}";
		}
		return null;
	}

	public void Delete(int id)
	{
		try
		{
			var subject = Get(id);
			connection.Delete(subject);
		}
		catch (Exception ex)
		{
			StatusMessage = $"Error: {ex.Message}";
		}
	}
}
