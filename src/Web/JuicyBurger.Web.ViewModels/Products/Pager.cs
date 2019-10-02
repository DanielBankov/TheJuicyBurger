using System;

namespace JuicyBurger.Web.ViewModels.Products
{
    public class Pager
    {
        public Pager(int totalProducts, int page = 1) 
        {
            var pageSize = 3.0;
            var totalPages = (int)Math.Ceiling(totalProducts / pageSize);
            var currentPage = page;
            var firstPage = page - 3;
            var lastPage = page + 3;

            if (firstPage <= 0)
            {
                firstPage = 1;
            }

            if(lastPage >= totalPages)
            {
                lastPage = totalPages + 1;
            }

            this.TotalPages = totalPages;
            this.CurrentPage = currentPage;
            this.FirstPage = firstPage;
            this.Lastpage = lastPage;
        }

        public int TotalPages { get; set; }

        public int FirstPage { get; set; }

        public int Lastpage { get; set; }

        public int CurrentPage { get; set; }
    }
}
