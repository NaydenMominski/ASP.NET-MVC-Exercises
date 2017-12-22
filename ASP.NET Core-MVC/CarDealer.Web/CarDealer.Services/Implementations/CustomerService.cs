
namespace CarDealer.Services.Implementations
{
    using Data;
    using Data.Models;
    using System.Collections.Generic;
    using Models;
    using Models.Sales;
    using Models.Customers;
    using System.Linq;
    using System;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }


        public void Create(string name, DateTime birthday, bool isYoungDriver)
        {
            var customer = new Customer
            {
                Name = name,
                BirthDate = birthday,
                IsYoungDriver = isYoungDriver
            };

            this.db.Add(customer);
            this.db.SaveChanges();
        }

        public void Edit(int id, string name, DateTime birthDay, bool isYoungDriver)
        {
            var existingCustomer = this.db.Customers.Find(id);

            if (existingCustomer==null)
            {
                return;
            }

            existingCustomer.Name = name;
            existingCustomer.BirthDate = birthDay;
            existingCustomer.IsYoungDriver = isYoungDriver;

            this.db.SaveChanges();

        }

        public IEnumerable<CustomerModel> Ordered(OrderDirection order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery
                        .OrderBy(c => c.BirthDate)
                        .ThenBy(c => c.Name);
                    break;
                case OrderDirection.Descending:
                    customersQuery = customersQuery
                        .OrderByDescending(c => c.BirthDate)
                        .ThenBy(c => c.Name);
                    break;
                default:
                    throw new System.InvalidOperationException($"Invalid order direction: {order}.");                  
            }
            return customersQuery
                .Select(c => new CustomerModel
                {
                    Id =c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();
        }

        public CustomerModel ById(int id)
            => this.db
            .Customers
            .Where(c => c.Id == id)
            .Select(c => new CustomerModel
            {
                Id = c.Id,
                Name = c.Name,
                BirthDate = c.BirthDate,
                IsYoungDriver = c.IsYoungDriver
            })
            .FirstOrDefault();


        public CustomerTotalSalesModel TotalSaleById(int id)
        => this.db
            .Customers
            .Where(c => c.Id == id)
            .Select(c => new CustomerTotalSalesModel
            {
                Name = c.Name,

                IsYoungDriver = c.IsYoungDriver,
                BoughtCars = c.Sales.Select(s=>new SaleModel
                {
                    Price= s.Car.Parts.Sum(p=>p.Part.Price),
                    Discount = s.Discount
                })
            })
            .FirstOrDefault();

        public bool Exists(int id)
            => this.db.Customers.Any(c => c.Id == id);
    }
}
