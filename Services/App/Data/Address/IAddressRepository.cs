using Device.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data.Address
{
    public interface IAddressRepository : IAppRepository
    {
        Task<Database.Entities.Address> GetAddress(int id);
        Task<IEnumerable<Database.Entities.Address>> GetAddresses(AddressParams addressParams);
    }
}
