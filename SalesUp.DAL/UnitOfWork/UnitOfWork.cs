using SalesUp.DAL.Entity;
using SalesUp.DAL.Factory;
using System;

namespace SalesUp.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private SalesUpEntities dbContext;

        public UnitOfWork()
        {
            this.dbContext = new SalesUpEntities();
        }

        public UnitOfWork(SalesUpEntities dataContext)
        {
            this.dbContext = dataContext;
        }

        public SalesUpEntities DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit()
        {
            try
            {
                dbContext.Commit();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
            }
        }
    }
}
