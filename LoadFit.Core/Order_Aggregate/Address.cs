using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core.Order_Aggregate
{
    public class Address
    {
        public Address()
        {
            
        }

        public Address(string firstName, string lastName, string city, string street, string country, string pickupLocation, string destinationLocation)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            Country = country;
            PickupLocation = pickupLocation;
            DestinationLocation = destinationLocation;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string PickupLocation { get; set; }
        public string DestinationLocation { get; set; }
    }
}
