using Broker.Model;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Service
{
    public class BrokerService : IBrokerService
    {
        private DataContext _dataContext;
        public List<AddressModel> Addresses { get; set; }
        public BrokerService()
        {
            _dataContext = new DataContext();

            Addresses = GetAddress();
        }

        private List<AddressModel> GetAddress()
        {
            return _dataContext.Addresses.Select(x => new AddressModel
            {
                MacAddress = x.MacAddress,
                Id = x.Id,
            }).ToList();
        }

        private void RefreshAddress()
        {
            if (Addresses.Count != _dataContext.Addresses.Count())
                Addresses = GetAddress();
        }

        public async Task<bool> AddAddresses(List<RedisValueModel> jsonValues)
        {
            RefreshAddress();

            var scanners = jsonValues.Select(x => x.Name).Distinct().ToArray();

            var trackers = jsonValues.Select(x => x.MacAddress).Distinct().ToArray();

            var values = scanners.Concat(trackers).Distinct().ToList();

            var addressToAdd = values.Where(value => !Addresses.Any(x => x.MacAddress == value)).Select(x => new Address()
            {
                Created = DateTime.Now,
                MacAddress = x,
                IsConfirmed = false,
            }).ToList();

            if (addressToAdd.Any())
                await _dataContext.Addresses.AddRangeAsync(addressToAdd);

            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> SaveValues(List<RedisValueModel> jsonValues)
        {
            RefreshAddress();

            var registrations = new List<Registration>();

            jsonValues.ForEach(value =>
            {
                var scannerAddressId = Addresses.FirstOrDefault(x => x.MacAddress == value.Name)?.Id;

                var trackerAddressId = Addresses.FirstOrDefault(x => x.MacAddress == value.MacAddress)?.Id;

                if (scannerAddressId.HasValue && trackerAddressId.HasValue)
                    registrations.Add(new Registration
                    {
                        Created = value.Time,
                        Rssi = value.Rssi,
                        ScannerAddressId = scannerAddressId.Value,
                        TrackerAddressId = trackerAddressId.Value,
                    });
            });

            if (registrations.Any())
                await _dataContext.Registrations.AddRangeAsync(registrations);

            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
