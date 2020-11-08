using Model.Models;
using Model.Models.House;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IHouseManager
    {
        Task<bool> ExistsAsync(int residentialId, int houseId);

        Task<bool> ExistsAsync(int residentialId, string houseName);

        Task<bool> ExistsAsync(int residentialId, int houseId, string houseName);

        Task<PaginatedList<House>> GetAllAsync(int residentialId, Parameter parameter);

        Task<HouseToUpdate> GetByIdAsync(int id);

        Task UpdateAsync(HouseToUpdate model);

        Task AddAsync(HouseToAdd model);

        Task DeleteAsync(int id);
    }
}
