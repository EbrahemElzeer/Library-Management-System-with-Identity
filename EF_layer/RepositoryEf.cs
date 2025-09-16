using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;

namespace EF_layer
{
    public class RepositoryEf<T> : IBaseRepository<T> where T : class

    {
        protected AppDbContext appDbContext;
        public RepositoryEf(AppDbContext _appDbContext)
        {
            appDbContext=_appDbContext;
        }

        public async Task<T> AddAsync(T Item)
        {
            await appDbContext.Set<T>().AddAsync(Item);
            return Item;
        }

        public void  Delete(T Item)
        {
            appDbContext.Set<T>().Remove(Item);
        }

        public async Task<T> EditAsync( T Item)
        {

           appDbContext.Set<T>().Update(Item);
            await appDbContext.SaveChangesAsync();
            return Item;
        }

        public T FindById(int id)
        {
          return  appDbContext.Set<T>().Find(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await appDbContext.Set<T>().ToListAsync();
        }

       

        public async Task<int> SaveChangesAsync()
        {
            return await appDbContext.SaveChangesAsync();
        }

    }
}
