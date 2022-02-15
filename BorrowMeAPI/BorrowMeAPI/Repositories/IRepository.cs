﻿using System.Linq.Expressions;

namespace BorrowMeAPI.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> GetByProperty(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        void Delete(T entity);
        Task<T> Edit(T entity);
    }

}
