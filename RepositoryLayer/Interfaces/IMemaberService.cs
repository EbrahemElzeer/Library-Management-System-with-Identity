using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Managment_System.Models;

namespace ApplicationCore.Interfaces
{
    public interface IMemberService
    {
        
        Task<IEnumerable<Member>> GetAllAsync();
        Task SaveAdd(Member member);
        Task Delete(int id);
        Task<Member> FindById(int id);
        Task SaveChangesAsync();
    }
}
