using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SalesUp.DAL.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        // Mars an entity as new
        void Insert(T entity);
        // Marks an entity as modified
        void Update(T entity);
        // Marks an entity to be removed
        void Delete(T entity);
        // Delete an entity using delegate
        void Delete(Expression<Func<T, bool>> where);
        // Get an entity by int id
        T GetById(object id);
        // Get an entity using delegate
        T GetByIdentity(Expression<Func<T, bool>> where);
        // Gets all entities of the type T
        IEnumerable<T> GetAll();
        // Gets entities using delegate
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    }
}
