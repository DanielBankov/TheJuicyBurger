using AutoMapper;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Receipts;
using System;
using System.Linq;

namespace JuicyBurger.Web.ViewModels.Receipt
{
    public class ReceiptAllViewModel : IMapFrom<ReceiptServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public int ProductsQty { get; set; }

        public decimal Total { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
              .CreateMap<ReceiptServiceModel, ReceiptAllViewModel>()
              .ForMember(destination => destination.Total,
                          opts => opts.MapFrom(origin => origin.Orders.Sum(order => order.Product.Price * order.Quantity)))
              .ForMember(destination => destination.ProductsQty,
                          opts => opts.MapFrom(origin => origin.Orders.Sum(order => order.Quantity)));
        }
    }
}
