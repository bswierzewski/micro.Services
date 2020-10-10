using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data.Registrations
{
    public interface IRegistrationRepository : IAppRepository
    {
        Task<ICollection<Database.Entities.Registration>> GetRegistrations(int macAddressId);
    }
}
