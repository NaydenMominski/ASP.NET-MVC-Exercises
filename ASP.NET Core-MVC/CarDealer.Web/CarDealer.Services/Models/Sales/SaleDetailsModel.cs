using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services.Models.Sales
{
    using Cars;

    public class SaleDetailsModel:SaleListModel
    {
        public CarModel Car { get; set; }
    }
}
