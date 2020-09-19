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

        public void SaveAddresses(HashSet<string> addresses)
        {
            var databaseAddresses = _dataContext.Addresses.Select(x => x.Label).ToHashSet();

            addresses.ExceptWith(databaseAddresses);

            var addressesToSave = new List<Database.Entities.Address>();

            addresses.ToList().ForEach(address =>
            {
                addressesToSave.Add(new Database.Entities.Address()
                {
                    Created = DateTime.Now,
                    IsConfirmed = false,
                    Label = address,
                });
            });

            if (addressesToSave.Any())
            {
                _dataContext.AddRange(addressesToSave);

                _dataContext.SaveChanges();
            }
        }

        public void SaveValues(List<ValueModel> jsonValues)
        {
            var addresses = _dataContext.Addresses.ToList();

            var registrations = new List<Database.Entities.Registration>();

            jsonValues.ForEach(json =>
            {
                var macAddressId = addresses.FirstOrDefault(x => x.Label == json.MacAddress)?.Id;

                var bleAddressId = addresses.FirstOrDefault(x => x.Label == json.BleAddress)?.Id;

                if (macAddressId.HasValue && bleAddressId.HasValue)
                    registrations.Add(new Database.Entities.Registration()
                    {
                        Created = json.Time,
                        BleAddressId = bleAddressId.Value,
                        MacAddressId = macAddressId.Value,
                        Rssi = json.Rssi
                    });
            });

            if (registrations.Any())
            {
                _dataContext.Registrations.AddRange(registrations);

                _dataContext.SaveChanges();
            }
        }
    }
}
