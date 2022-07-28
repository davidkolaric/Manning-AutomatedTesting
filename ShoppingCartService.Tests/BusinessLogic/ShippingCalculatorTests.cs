using ShoppingCartService.BusinessLogic;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Models;
using Xunit;

namespace ShoppingCartService.Tests.BusinessLogic
{
    public class ShippingCalculatorTests
    {
        private readonly ShippingCalculator _shippingCalculator;

        public ShippingCalculatorTests()
        {
            _shippingCalculator = new ShippingCalculator();
        }

        [Fact]
        public void CalculateShippingCostWhenEmptyCartShouldReturn0()
        {
            var cart = new Cart
            {
                ShippingAddress = new Address()
            };

            var actual = _shippingCalculator.CalculateShippingCost(cart);

            Assert.Equal(0d, actual);
        }

        [Theory]
        [InlineData(CustomerType.Standard, ShippingMethod.Expedited, "Slovenia", "Ptuj", 1, 18)]
        [InlineData(CustomerType.Standard, ShippingMethod.Express, "Slovenia", "Ptuj", 1, 37.5)]
        [InlineData(CustomerType.Standard, ShippingMethod.Priority, "Slovenia", "Ptuj", 1, 30)]
        [InlineData(CustomerType.Standard, ShippingMethod.Standard, "Slovenia", "Ptuj", 1, 15)]
        [InlineData(CustomerType.Premium, ShippingMethod.Expedited, "Slovenia", "Ptuj", 1, 15)]
        [InlineData(CustomerType.Premium, ShippingMethod.Express, "Slovenia", "Ptuj", 1, 37.5)]
        [InlineData(CustomerType.Premium, ShippingMethod.Priority, "Slovenia", "Ptuj", 1, 15)]
        [InlineData(CustomerType.Premium, ShippingMethod.Standard, "Slovenia", "Ptuj", 1, 15)]
        public void CalculateShippingCost_ShouldReturnCorrectAmount(CustomerType customerType, ShippingMethod shippingMethod, string country, string city, uint itemsCount, double amount)
        {
            var cart = new Cart
            {
                CustomerType = customerType,
                ShippingMethod = shippingMethod,
                ShippingAddress = new Address
                {
                    City = city,
                    Country = country
                }
            };
            cart.Items.Add(new Item { Quantity = itemsCount });

            var actual = _shippingCalculator.CalculateShippingCost(cart);

            Assert.Equal(amount, actual);
        }
    }
}
