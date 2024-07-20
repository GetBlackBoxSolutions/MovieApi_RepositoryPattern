﻿using System.Linq.Expressions;

namespace ApplicationCore.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);

        Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "");

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
    }
}
