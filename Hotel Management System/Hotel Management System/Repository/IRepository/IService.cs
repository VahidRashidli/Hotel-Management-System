using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel_Management_System.Repository.IRepository
{
    interface IService<T>
    {
        void Create(T item);
        List<T> GetAll(T item);
        void Update(int Id, T item);
        void Delete(T item,int Id);
    }
}
