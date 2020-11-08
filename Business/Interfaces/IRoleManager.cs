using Model.Models.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IRoleManager
    {
        Task<IEnumerable<Role>> GetResidentialRolesAsync();
    }
}
