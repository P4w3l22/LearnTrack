using LearnTrack.MVVM.Models.Base;
using System.Linq.Expressions;

namespace LearnTrack.Repositories.IRepository;

public interface IBaseRepository<T> : IDisposable where T : TableData, new()
{
	void UpsertItem(T item);
	T GetItem(int id);
	T GetItem(Expression<Func<T, bool>> predicate);
	List<T> GetItems();
	List<T> GetItems(Expression<Func<T, bool>> predicate);
	void DeleteItem(T item);
}
