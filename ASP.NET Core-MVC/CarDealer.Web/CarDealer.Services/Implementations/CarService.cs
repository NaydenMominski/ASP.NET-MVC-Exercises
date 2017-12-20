﻿
namespace CarDealer.Services.Implementations
{
    using Data;
    using System.Collections.Generic;
    using Models.Cars;
    using System.Linq;
    using Models;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;

        public  CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CarModel> ByMake(string make)
            =>this.db
                 .Cars
                 .Where(c => c.Make.ToLower() == make.ToLower())
                 .OrderBy(c => c.Model)
                 .ThenBy(c => c.TravelledDistance)
                 .Select(c => new CarModel
                 {
                     Make = c.Make,
                     Model = c.Model,
                     TravelledDistance = c.TravelledDistance
                 })
                 .ToList();

        public IEnumerable<CarWithPartsModel> WithParts()
            => this.db
            .Cars
            .Select(c => new CarWithPartsModel
            {
                Make = c.Make,
                Model =c.Model,
                TravelledDistance =c.TravelledDistance,
                Parts = c.Parts.Select(p => new PartModel
                {
                    Name = p.Part.Name,
                    Price = p.Part.Price
                })
            })
            .ToList();
    }
}
