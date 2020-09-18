using Database;
using Device.Params;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Device.Data.Address
{
    public class AddressRepository : AppRepository, IAddressRepository
    {
        public AddressRepository(DataContext context) : base(context) { }

        public async Task<Database.Entities.Address> GetAddress(int id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Database.Entities.Address>> GetAddresses(AddressParams addressParams)
        {
            Expression<Func<Database.Entities.Address, bool>> @where = n => true;

            if (addressParams.IsConfirmed.HasValue)
                @where = n => n.IsConfirmed == addressParams.IsConfirmed.Value;

            return await _context.Addresses.Where(@where).ToListAsync();
        }
    }
}
