using API.DTOs;
using Core.Entities;

namespace API.Extensions
{
    public static class AddressMappingExtensions
    {
        public static AddressDTO? ToDto(this Address address)
        {
            if (address == null) return null;

            return new AddressDTO
            {
                Line1 = address.Line1,
                City = address.City,
                Country = address.Country,
                Line2 = address.Line2,
                PostalCode = address.PostalCode,
                State = address.State,
            };
        }

        public static Address ToEntity(this AddressDTO addressDTO)
        {
            if (addressDTO == null) throw new ArgumentNullException(nameof(addressDTO));

            return new Address
            {
                Line1 = addressDTO.Line1,
                City = addressDTO.City,
                Country = addressDTO.Country,
                Line2 = addressDTO.Line2,
                PostalCode = addressDTO.PostalCode,
                State = addressDTO.State,
            };
        }

        public static void UpdateFromDTO(this Address address, AddressDTO addressDTO)
        {
            if (addressDTO == null) throw new ArgumentNullException(nameof(addressDTO));
            if (address == null) throw new ArgumentNullException(nameof(address));

            address.Line1 = addressDTO.Line1;
            address.City = addressDTO.City;
            address.Country = addressDTO.Country;
            address.Line2 = addressDTO.Line2;
            address.PostalCode = addressDTO.PostalCode;
            address.State = addressDTO.State;
        }
    }
}