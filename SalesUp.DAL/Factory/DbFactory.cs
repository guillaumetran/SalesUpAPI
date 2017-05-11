using SalesUp.DAL.Entity;

namespace SalesUp.DAL.Factory
{
    public class DbFactory : Disposable, IDbFactory
    {
        SalesUpEntities dbContext;

        public SalesUpEntities Init()
        {
            return dbContext ?? (dbContext = new SalesUpEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}