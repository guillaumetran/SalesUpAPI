using System.Collections.Generic;

namespace SalesUp.BLL
{
    public interface IGenericService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}