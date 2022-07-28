using ShoppingCartService.BusinessLogic.Validation;
using ShoppingCartService.Models;
using Xunit;

namespace ShoppingCartService.Tests.BusinessLogic.Validation
{
    public class AddressValidatorTests
    {
        private readonly AddressValidator _validator;

        public AddressValidatorTests()
        {
            _validator = new AddressValidator();
        }

        [Fact]
        public void IsValid_ValidValues_ReturnsTrue()
        {
            var address = new Address
            {
                City = "City",
                Country = "Country",
                Street = "Street"
            };

            var actual = _validator.IsValid(address);

            Assert.True(actual);
        }

        [Fact]
        public void IsValid_WhenCityIsEmpty_ReturnsFalse()
        {
            var address = new Address
            {
                Country = "Country",
                Street = "Street"
            };

            var actual = _validator.IsValid(address);

            Assert.False(actual);
        }

        [Fact]
        public void IsValid_WhenCountryIsEmpty_ReturnsFalse()
        {
            var address = new Address
            {
                City = "City",
                Street = "Street"
            };

            var actual = _validator.IsValid(address);

            Assert.False(actual);
        }

        [Fact]
        public void IsValid_WhenStreetIsEmpty_ReturnsFalse()
        {
            var address = new Address
            {
                City = "City",
                Country = "Country"
            };

            var actual = _validator.IsValid(address);

            Assert.False(actual);
        }

        [Fact]
        public void IsValid_WhenAdressIsNull_ReturnsFalse()
        {
            Address address = null;

            var actual = _validator.IsValid(address);

            Assert.False(actual);
        }
    }
}
