using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ResidentialStatusRepository : IResidentialStatusRepository
    {
        private readonly ResidentialContext _ctx;
        public ResidentialStatusRepository(ResidentialContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<ResidentialStatusEf>> GetAllAsync()
        {
            return await _ctx.ResidentialStatus.AsNoTracking().ToListAsync();
        }
    }
}
