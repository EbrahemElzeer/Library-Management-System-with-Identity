using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOS
{
    public record AssignRoleDto{


        public string UserId { get; init; }
        public string RoleName { get; init; }
        
    }
  
}
