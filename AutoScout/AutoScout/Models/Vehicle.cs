using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoScout.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string VIN { get; set; }
        public string StockNumber { get; set; }
        public string Mileage { get; set; }
        public string Transmission { get; set; }
        public string Style { get; set; }
        public string ExteriorColor { get; set; }
        public string InteriorColor { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public ICollection<VehicleImage> Images { get; set; }
        public VehicleImage DefaultImage { get; set; }
        public virtual Dealership Dealership { get; set; }
        public int DealershipId { get; set; }

        Vehicle()
        {
            var context = new AutoScoutDBContext();
            var imagesFromDb = context.VehicleImages.Where(x => x.VehicleId == this.Id);
            foreach(var image in imagesFromDb)
            {
                this.Images.Add(image);
            }
        }

    }

    
}