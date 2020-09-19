using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Device.Data.Registrations
{
    public class RegistrationRepository : AppRepository, IRegistrationRepository
    {
        public RegistrationRepository(DataContext context) : base(context) { }

        public async Task<ICollection<Database.Entities.Registration>> GetRegistrations(int macAddressId)
        {
            return await _context.Registrations
                .Include(x => x.BleAddress)
                .Include(x => x.MacAddress).ToListAsync();
        }
    }
}
