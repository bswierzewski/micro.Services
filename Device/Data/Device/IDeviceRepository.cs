using Device.Dtos;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Device.Data
{
    public interface IDeviceRepository : IAppRepository
    {
        IQueryable<GetDeviceDto> GetDevices(Expression<Func<Database.Entities.Device, bool>> expressionDevice = null);
        Task<bool> ExistsDevice(string macAddress);
        Task<Database.Entities.Device> GetDevice(int deviceId);
    }
}