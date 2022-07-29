using AutoMapper;
using ShoppingCartService.BusinessLogic;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Mapping;
using ShoppingCartService.Models;
using Xunit;

namespace ShoppingCartService.Tests.BusinessLogic
{
    public class CheckOutEngineTests
    {
        private readonly IMapper _mapper;
        private readonly ShippingCalculator _shippingCalculator;
        public CheckOutEngineTests()
        {
            _shippingCalculator = new ShippingCalculator();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void CalculateTotals_WhenStandardCustomer_TotalsEqualsCostPlusShipping()
        {
            var checkOutEngine = new CheckOutEngine(_shippingCalculator, _mapper);
            var cart = new Cart
            {
                ShippingAddress = new Address(),
            };
            cart.Items.Add(new Item { Price = 10, Quantity = 3 });

            var actual = checkOutEngine.CalculateTotals(cart).Total;

            Assert.Equal(75, actual);
        }

        [Fact]
        public void CalculateTotals_WhenStandardCustomer_NoDiscount()
        {
            var checkOutEngine = new CheckOutEngine(_shippingCalculator, _mapper);
            var cart = new Cart
            {
                ShippingAddress = new Address(),
            };
            cart.Items.Add(new Item { Price = 10, Quantity = 3 });

            var actual = checkOutEngine.CalculateTotals(cart).CustomerDiscount;

            Assert.Equal(0, actual);
        }

        [Fact]
        public void CalculateTotals_WhenPremiumCustomer_TotalsEqualsCostPlusShippingMultiplyWithDiscount()
        {
            var checkOutEngine = new CheckOutEngine(_shippingCalculator, _mapper);
            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingAddress = new Address(),
            };
            cart.Items.Add(new Item { Price = 10, Quantity = 3 });

            var actual = checkOutEngine.CalculateTotals(cart).Total;

            Assert.Equal(67.5, actual);
        }

        [Fact]
        public void CalculateTotals_WhenPremiumCustomer_HasDiscount()
        {
            var checkOutEngine = new CheckOutEngine(_shippingCalculator, _mapper);
            var cart = new Cart
            {
                CustomerType = CustomerType.Premium,
                ShippingAddress = new Address(),
            };
            cart.Items.Add(new Item { Price = 10, Quantity = 3 });

            var actual = checkOutEngine.CalculateTotals(cart).CustomerDiscount;

            Assert.NotEqual(0, actual);
        }
    }
}
