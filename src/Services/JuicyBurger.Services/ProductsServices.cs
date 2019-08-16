using JuicyBurger.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuicyBurger.Services
{
    public class ProductsServices : IProductsServices
    {
        private readonly JuicyBurgerDbContext context;

        public ProductsServices(JuicyBurgerDbContext context)
        {
            this.context = context;
        }

        public IQueryable All()
        {
           return this.context.Products.Select(x => x);
        }

        public bool Create()
        {
            throw new NotImplementedException();
        }
    }
}
