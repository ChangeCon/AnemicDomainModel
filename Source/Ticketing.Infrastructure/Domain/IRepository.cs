namespace Ticketing.Infrastructure.Domain
{
	public interface IRepository<T, TId> : IReadOnlyRepository<T, TId>, IRootRepository<T, TId> where T : IAggregateRoot
	{
		T Save(T entity);
		T Add(T entity);
		void Remove(T entity);
	}
}