using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IBaseRepository <T> where T : class
    {
        T FindById (int id);

        Task <T> EditAsync (T Item);

        void Delete(T Item);

        Task<T> AddAsync(T Item);

        Task <List<T>> GetAllAsync ();

         Task <int> SaveChangesAsync();



    }
}
