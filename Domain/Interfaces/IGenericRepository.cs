using System;
using System.Linq.Expressions;

namespace Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
        T? GetById(Guid? id);
        T GetByIdOrDefault(Guid? id);
        IQueryable<T> Query();
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}

