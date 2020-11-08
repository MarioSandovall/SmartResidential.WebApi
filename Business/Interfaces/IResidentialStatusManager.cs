using Model.Models.Residential;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IResidentialStatusManager
    {
        Task<IEnumerable<ResidentialStatus>> GetAllAsync();
    }
}
