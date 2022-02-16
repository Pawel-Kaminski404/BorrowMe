﻿using System.Linq.Expressions;

namespace BorrowMeAPI.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> GetByProperty(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task<T> Delete(T entity);
        Task<T> Edit(T entity);
    }

}
