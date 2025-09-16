using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOS
{
    public record RoleDto
    {
        public string RoleId { get; init; }
        public string RoleName { get; init; }
    }


}
