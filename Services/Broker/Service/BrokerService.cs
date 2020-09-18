using Broker.Model;
using Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Broker.Service
{
    public class BrokerService : IBrokerService
    {
        private DataContext _dataContext;
        public BrokerService()
        {
            _dataContext = new DataContext();
        }
        public async void AddToTempDevices(List<RedisValueModel> jsonValues)
        {
            var scanners = jsonValues.Select(x => x.Name).Distinct().ToList();

            var trackers = jsonValues.Select(x => x.MacAddress).Distinct().ToList();

            scanners.ForEach(scanner =>
            {
                if (!_dataContext.TempDevices.Any(x => x.MacAddress == scanner))
                    _dataContext.TempDevices.Add(new Database.Entities.TempDevice
                    {
                        Created = DateTime.Now,
                        TypeId = (short)Database.Enums.TempDeviceType.scanner,
                        MacAddress = scanner,
                        IsConfirmed = false
                    });
            });

            trackers.ForEach(tracker =>
            {
                if (!_dataContext.TempDevices.Any(x => x.MacAddress == tracker))
                    _dataContext.TempDevices.Add(new Database.Entities.TempDevice
                    {
                        Created = DateTime.Now,
                        TypeId = (short)Database.Enums.TempDeviceType.tracker,
                        MacAddress = tracker,
                        IsConfirmed = false,
                    });
            });

            await _dataContext.SaveChangesAsync();
        }

        public async void SaveValues(List<RedisValueModel> jsonValues)
        {
            jsonValues.ForEach(value =>
            {
                _dataContext.Registrations.Add(new Database.Entities.Registration
                {
                    Created = value.Time,
                    MacAddress = value.MacAddress,
                    Name = value.Name,
                    Rssi = value.Rssi
                });
            });

            await _dataContext.SaveChangesAsync();
        }
    }
}
