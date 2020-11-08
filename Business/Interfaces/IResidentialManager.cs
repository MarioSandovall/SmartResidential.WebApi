using Model.Models;
using Model.Models.Residential;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IResidentialManager
    {
        Task<bool> ExistsAsync(int residentialId);

        Task<PaginatedList<Residential>> GetAllAsync(Parameter parameter);

        Task<ResidentialToUpdate> GetByIdAsync(int residentialId);

        Task UpdateAsync(ResidentialToUpdate model);

        Task AddAsync(ResidentialToAdd model);
    }
}
