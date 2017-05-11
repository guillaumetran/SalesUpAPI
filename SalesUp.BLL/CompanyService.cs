using SalesUp.DAL;
using SalesUp.DAL.Entity;
using SalesUp.DAL.Repository;
using SalesUp.DAL.UnitOfWork;

namespace SalesUp.BLL
{
    public class CompanyService : GenericService<Company>, ICompanyService
    {
        IGenericRepository<Company> genericRepository = new GenericRepository<Company>();
        IUnitOfWork unitOfWork = new UnitOfWork();

        public CompanyService()
        {
            this.genericRepository = new GenericRepository<Company>();
            this.unitOfWork = new UnitOfWork();
        }

        public CompanyService(GenericRepository<Company> genericRepository, UnitOfWork unitOfWork)
        {
            this.genericRepository = genericRepository;
            this.unitOfWork = unitOfWork;
        }
    }
}