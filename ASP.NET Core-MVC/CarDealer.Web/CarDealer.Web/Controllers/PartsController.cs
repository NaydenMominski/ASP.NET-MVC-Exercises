﻿namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Parts;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PartsController:Controller
    {
        private const int PageSize = 25;


        private readonly IPartService parts;

        private readonly ISupplierService suppliers;

        public PartsController(IPartService parts, ISupplierService suppliers)
        {
            this.parts = parts;
            this.suppliers = suppliers;
        }

        public IActionResult Create() 
            => this.View(new PartFormModel
            {
                Suppliers=GetSupplierListItems()          
            });

        [HttpPost]
        public IActionResult Create(PartFormModel model)
        {

            if (!ModelState.IsValid)
            {
                model.Suppliers = this.GetSupplierListItems();

                return View(model);
            }
            this.parts.Create(
                model.Name,
                model.Price,
                model.Quantity,
                model.SupplierId);

            return RedirectToAction(nameof(All));

        }

        public IActionResult Edit(int id)
        {
            var part = this.parts.ById(id);

            if (part==null)
            {
                return NotFound();
            }

            return View(new PartFormModel
            {
                Name=part.Name,
                Price=part.Price,
                Quantity=part.Quantity,
                IsEdit=true
                
            });
        }

        [HttpPost]
        public IActionResult Edit(int id,PartFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.IsEdit = true;
                return View(model);
            }

            this.parts.Edit(
                id,
                model.Price,
                model.Quantity);

            return RedirectToAction(nameof(All));


        }

        public IActionResult Delete(int id) => View(id);

        public IActionResult Destroy(int id)
        {
            this.parts.Delete(id);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All(int page = 1)
            => View(new PartPageListingModel
            {
                Parts=this.parts.AllListings(page, PageSize),
                CurrentPage=page,
                TotalPages=(int)Math.Ceiling(this.parts.Total()/(double)PageSize)

            });
        private IEnumerable<SelectListItem> GetSupplierListItems()
             =>this.suppliers
                .All()
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                });
       
    }
}
