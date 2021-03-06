﻿
namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Models.Cars;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;

    [Route("cars")]
    public class CarsController: Controller
    {
        private readonly ICarService cars;
        private readonly IPartService parts;

        public CarsController(ICarService cars,IPartService parts)
        {
            this.cars = cars;
            this.parts=parts;
        }

        [Authorize]
        [Route(nameof(Create))]
        public IActionResult Create() 
            => View(new CarFormModel
            {
                AllParts=this.GetPartSelectItems()
            });

        [Authorize]
        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CarFormModel carModel)
        {
            if (!ModelState.IsValid)
            {
                carModel.AllParts = this.GetPartSelectItems();
                return View(carModel);
            }

            this.cars.Create(
                carModel.Make,
                carModel.Model,
                carModel.TravelledDistance,
                carModel.SelectedParts);
                        
            return RedirectToAction(nameof(Parts));
        }

        [Route("{make}", Order =2)]
        public IActionResult ByMake(string make)
        {
            var cars = this.cars.ByMake(make);

            return View(new CarsByMakeModel
            {
                Make = make,
                Cars = cars
            });
        }

        [Route("parts", Order = 1)]
        public IActionResult Parts()
            => View(this.cars.WithParts());

        private IEnumerable<SelectListItem> GetPartSelectItems()
            => this.parts
                .All()
                .Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                });

    }
}
