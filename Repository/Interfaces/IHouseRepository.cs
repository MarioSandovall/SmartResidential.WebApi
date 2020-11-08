using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IHouseRepository
    {
        Task<bool> ExistsAsync(int residentialId, int houseId);

        Task<bool> ExistsAsync(int residentialId, string houseName);

        Task<bool> ExistsAsync(int residentialId, int houseId, string houseName);

        IQueryable<HouseEf> AsNoTracking();

        Task<HouseEf> GetByIdAsync(int houseId);

        Task AddAsync(HouseEf entity);

        Task UpdateAsync(HouseEf entity);

        Task DeleteAsync(int houseId);
    }
}
