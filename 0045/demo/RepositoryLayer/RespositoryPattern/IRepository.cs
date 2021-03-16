using System;
using System.Collections.Generic;
using demo.DomainLayer.Models;

namespace demo.RepositoryLayer.RespositoryPattern
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(int Id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}
