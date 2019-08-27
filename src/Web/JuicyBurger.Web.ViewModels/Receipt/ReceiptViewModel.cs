using AutoMapper;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Receipts;
using System;
using System.Collections.Generic;

namespace JuicyBurger.Web.ViewModels.Receipt
{
    public class ReceiptViewModel : IMapFrom<ReceiptServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public DateTime IssuedOn;

        public string RecipientId { get; set; }

        public string Recipient { get; set; }

        public List<ReceiptDetailsOrderViewModel> Orders { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
               .CreateMap<ReceiptServiceModel, ReceiptViewModel>()
               .ForMember(destination => destination.Recipient,
                           opts => opts.MapFrom(origin => origin.Recipient.UserName));
        }
    }
}
