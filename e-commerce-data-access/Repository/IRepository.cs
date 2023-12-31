﻿using System.Linq.Expressions;

namespace e_commerce_data_access.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Add(T entity);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
    }
}
