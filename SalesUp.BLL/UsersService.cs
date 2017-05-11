using SalesUp.DAL;
using SalesUp.DAL.Entity;
using SalesUp.DAL.Repository;
using SalesUp.DAL.UnitOfWork;

namespace SalesUp.BLL
{
    public class UsersService : GenericService<Users>, IUsersService
    {
        IGenericRepository<Users> genericRepository = new GenericRepository<Users>();
        IUnitOfWork unitOfWork = new UnitOfWork();

        public UsersService()
        {
            this.genericRepository = new GenericRepository<Users>();
            this.unitOfWork = new UnitOfWork();
        }

        public UsersService(GenericRepository<Users> genericRepository, UnitOfWork unitOfWork)
        {
            this.genericRepository = genericRepository;
            this.unitOfWork = unitOfWork;
        }
    }
}