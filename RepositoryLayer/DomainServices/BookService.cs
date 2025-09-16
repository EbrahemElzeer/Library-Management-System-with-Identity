using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Library_Managment_System.Models;
using RepositoryLayer.Interfaces;

namespace ApplicationCore.DomainServices
{
    public class BookService:IBookService
    {

        private readonly IBaseRepository<Books> _BaseRepository;

        public BookService(IBaseRepository<Books> BaseRepository)
        {
            _BaseRepository = BaseRepository;
        }

        public async Task<IEnumerable<Books>> GetAllAsync()
        {
            return await _BaseRepository.GetAllAsync();
        }
        public async Task saveBook(Books books)
        {
            if (books.Id == 0)
            {
                await _BaseRepository.AddAsync(books);
                await _BaseRepository.SaveChangesAsync();
            }
            else
            {
                await _BaseRepository.EditAsync(books);
            
               await _BaseRepository.SaveChangesAsync();
              } 
        
        
        }

        public async Task DeleteBook(int id) 
        {
            var book = _BaseRepository.FindById(id);
            _BaseRepository.Delete(book);
            await _BaseRepository.SaveChangesAsync();
        }

        public async Task<Books> FindById(int id)
        {
           return _BaseRepository.FindById(id);
        }
        public async Task SaveChangesAsync()
        {
            await _BaseRepository.SaveChangesAsync();
        }
    }
}
