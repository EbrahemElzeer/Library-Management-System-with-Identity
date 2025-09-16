using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOS
{
    public record UserWithRoleDto{

    public string Id { get; init; }
    public string UserName { get; init; }
    public string RoleName { get; init; }
    public string Email { get; init; }
}
    }
