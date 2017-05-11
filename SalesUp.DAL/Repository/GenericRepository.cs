using SalesUp.DAL.Entity;
using SalesUp.DAL.Factory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SalesUp.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Properties

        private SalesUpEntities dataContext;
        private readonly IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected SalesUpEntities DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }

        #endregion

        public GenericRepository()
        {
            this.dataContext = new SalesUpEntities();
            this.dbSet = dataContext.Set<T>();
        }

        protected GenericRepository(SalesUpEntities dataContext)
        {
            this.dataContext = dataContext;
            this.dbSet = dataContext.Set<T>();
        }

        #region Implementation

        public void Insert(T entity)
        {
            try
            {
                dbSet.Add(entity);
                dataContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void Update(T entity)
        {
            try
            {
                dbSet.Attach(entity);
                dataContext.Entry(entity).State = EntityState.Modified;
                dataContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                dbSet.Remove(entity);
                dataContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            try
            {
                IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
                foreach (T obj in objects)
                    dbSet.Remove(obj);
                dataContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public T GetById(object id)
        {
            try
            {
                return dbSet.Find(id);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
            }
            return null;
        }

        public T GetByIdentity(Expression<Func<T, bool>> where)
        {
            try
            {
                return dbSet.Where(where).FirstOrDefault<T>();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
            }
            return null;
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return dbSet.ToList();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
            }
            return null;
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            try
            {
                return dbSet.Where(where).ToList();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
            }
            return null;
        }

        #endregion
    }
}
