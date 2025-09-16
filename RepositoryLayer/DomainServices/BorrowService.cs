using ApplicationCore.Interfaces;
using Library_Managment_System.Models;
using RepositoryLayer.Interfaces;

namespace ApplicationCore.DomainServices
{
    public class BorrowService:IBorrowService
    {
        private readonly IBaseRepository<Borrow> BaseRepository;

        public BorrowService(IBaseRepository<Borrow> BaseRepository)
        {
            this.BaseRepository = BaseRepository;
        }

        public async Task<IEnumerable<Borrow>> GetAllAsync()
        {
            return await BaseRepository.GetAllAsync();
        }

        public async Task AddSaveBorrow(Borrow borrow)
        {
            if (borrow.Id == 0)
            {
                await BaseRepository.AddAsync(borrow);
                await BaseRepository.SaveChangesAsync();
            }
            else
            {
                await BaseRepository.EditAsync(borrow);
                await BaseRepository.SaveChangesAsync();
            }
        }

        public  async Task <Borrow> FindById(int id){

            return  BaseRepository.FindById(id);
            }


        public async Task Delete(int id)
        {
            var Borrow =  BaseRepository.FindById(id);
              BaseRepository.Delete(Borrow);
            await BaseRepository.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await BaseRepository.SaveChangesAsync();
        }


    }
}
