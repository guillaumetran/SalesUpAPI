using SalesUp.DAL.Entity;
using System;

namespace SalesUp.DAL.Factory
{
    public interface IDbFactory : IDisposable
    {
        SalesUpEntities Init();
    }
}
