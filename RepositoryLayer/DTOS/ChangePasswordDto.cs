using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOS
{
    public record ChangePasswordDto(string UserName,string NewPassword);
   
}
