using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<RoleEf>> GetResidentialRolesAsync();
    }
}
