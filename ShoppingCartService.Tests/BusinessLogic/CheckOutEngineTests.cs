using AutoMapper;
using ShoppingCartService.BusinessLogic;
using ShoppingCartService.Controllers.Models;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Mapping;
using ShoppingCartService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [Theory]
        [InlineData()]
        public void CalculateTotalsShouldReturnCorrectTotal()
        {
            var checkOutEngine = new CheckOutEngine(_shippingCalculator, _mapper);
            var cart = new Cart {
                ShippingAddress = new Address(),
            };

            var actual = checkOutEngine.CalculateTotals(cart);
            var expected = new CheckoutDto(new ShoppingCartDto { ShippingAddress = cart.ShippingAddress, Items = Enumerable.Empty<ItemDto>() }, 0, 0, 0);

            Assert.Equal(expected, actual);
        }
    }
}
