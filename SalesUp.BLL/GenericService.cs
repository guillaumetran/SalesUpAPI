using SalesUp.DAL;
using SalesUp.DAL.Repository;
using SalesUp.DAL.UnitOfWork;
using System;
using System.Collections.Generic;

namespace SalesUp.BLL
{
    public abstract class GenericService<T> : IGenericService<T> where T : class
    {
        IGenericRepository<T> genericRepository;
        IUnitOfWork unitOfWork;

        public GenericService()
        {
            this.genericRepository = new GenericRepository<T>();
            this.unitOfWork = new UnitOfWork();
        }

        public GenericService(GenericRepository<T> genericRepository, UnitOfWork unitOfWork)
        {
            this.genericRepository = genericRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(T entity)
        {
            try
            {
                genericRepository.Insert(entity);
            }
            catch (System.Exception tEx)
            {
                throw tEx;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                genericRepository.Delete(entity);
            }
            catch (System.Exception tEx)
            {
                throw tEx;
            }
        }

        public T GetById(object id)
        {
            T node = null;

            try
            {
                node = genericRepository.GetById(id);
            }
            catch (System.Exception tEx)
            {
                throw tEx;
            }
            return node;
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> nodes = null;

            try
            {
                nodes = genericRepository.GetAll();
            }
            catch (System.Exception tEx)
            {
                throw tEx;
            }
            return nodes;
        }

        public void Update(T entity)
        {
            try
            {
                genericRepository.Update(entity);
            }
            catch (System.Exception tEx)
            {
                throw tEx;
            }
        }
    }
}