using Data;
using Domain.Entities;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class RoleRespository : IRoleRepository
    {
        private readonly ResidentialContext _ctx;
        public RoleRespository(ResidentialContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<RoleEf>> GetResidentialRolesAsync()
        {
            return await _ctx.Roles.AsNoTracking()
                .Where(x => x.Id != (int)RoleEnum.SuperAdmin).ToListAsync();
        }

    }
}
