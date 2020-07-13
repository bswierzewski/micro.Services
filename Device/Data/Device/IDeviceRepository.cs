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
        Task<bool> IsDevice(string macAddress);
        Task<Database.Entities.Device> GetDevice(int deviceId);
        Task<bool> IsDeviceType(short payload);
        Task<bool> IsDeviceKind(short payload);
    }
}