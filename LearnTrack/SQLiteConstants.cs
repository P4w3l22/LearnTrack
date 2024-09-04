using SQLite;

namespace LearnTrack;

public static class SQLiteConstants
{
	private const string DbFileName = "LearnTrackDb.db";
	public const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
	public static string DatabasePath
	{
		get =>
			Path.Combine(FileSystem.AppDataDirectory, DbFileName);
	}
}
