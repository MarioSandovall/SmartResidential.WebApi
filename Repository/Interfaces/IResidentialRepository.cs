using Domain.Entities;
using Model.Models.Residential;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IResidentialRepository
    {
        Task<bool> ExistsAsync(int residentialId);

        IQueryable<Residential> AsNoTracking();

        Task<ResidentialEf> GetByIdAsync(int residentialId);

        Task AddAsync(ResidentialEf entity);

        Task UpdateAsync(ResidentialEf entity);

    }
}
