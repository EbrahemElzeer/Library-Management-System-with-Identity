using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Managment_System.Models;

namespace ApplicationCore.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Books>>GetAllAsync();
        Task saveBook(Books books);
        Task DeleteBook(int id);
        Task<Books> FindById(int id);
        Task SaveChangesAsync();
    }
}
