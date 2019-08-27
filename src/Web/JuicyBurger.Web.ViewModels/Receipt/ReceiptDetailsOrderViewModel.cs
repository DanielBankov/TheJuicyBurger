using AutoMapper;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;
using JuicyBurger.Services.Models.Receipts;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuicyBurger.Web.ViewModels.Receipt
{
    public class ReceiptDetailsOrderViewModel : IMapFrom<OrderServiceModel>
    {
        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }

    }
}
