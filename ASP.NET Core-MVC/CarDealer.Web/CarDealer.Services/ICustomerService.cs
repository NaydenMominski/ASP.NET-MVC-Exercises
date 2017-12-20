namespace CarDealer.Services
{
    using Models;
    using Models.Customers;
    using System.Collections.Generic;

   public interface ICustomerService
    {
        IEnumerable<CustomerModel> Ordered(OrderDirection order);

        CustomerTotalSalesModel TotalSaleById(int id);

    }
}
