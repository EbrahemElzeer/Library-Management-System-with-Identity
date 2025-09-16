using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Managment_System.Models;

namespace ApplicationCore.Interfaces
{
    public interface IBorrowService
    {

        Task<IEnumerable<Borrow>> GetAllAsync();
        Task AddSaveBorrow(Borrow borrow);
        Task<Borrow> FindById(int id);
        Task Delete(int id);
        Task SaveChangesAsync();

    }
}
