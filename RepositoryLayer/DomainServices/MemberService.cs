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

    public class MemberService:IMemberService
    {
        private readonly IBaseRepository<Member> BaseRepository;

        public MemberService(IBaseRepository<Member> _BaseRepository)
        {
            BaseRepository = _BaseRepository;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
          return  await BaseRepository.GetAllAsync();
        }
        public async Task SaveAdd(Member member)
        {
            try
            {
                if (member.Id == 0)
                {
                    await BaseRepository.AddAsync(member);
                    await BaseRepository.SaveChangesAsync();
                }
                else
                {
                    await BaseRepository.EditAsync(member);
                    await BaseRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("   Wrong: " + ex.Message, ex);
            }

        }

        public async Task Delete(int id)
        {
            var item = BaseRepository.FindById(id);
            BaseRepository.Delete(item);
            await BaseRepository.SaveChangesAsync();
        }
        public async Task<Member> FindById(int id)
        {
            return BaseRepository.FindById(id);
        }
        public async Task SaveChangesAsync()
        {
            await BaseRepository.SaveChangesAsync();
        }
    }
}
