using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoScout.Models;

namespace AutoScout.Services
{
    public class ImageManagementService
    {
        private AutoScoutDBContext db;

        public ImageManagementService(AutoScoutDBContext dbContext)
        {
            db = dbContext;
        }

        public void AssignImageToVehicle(int vehicleId, HttpPostedFileBase imageFile)
        {
            var vehicleImage = new VehicleImage();
            vehicleImage.VehicleId = vehicleId;
            vehicleImage.ImageBytes = new byte[imageFile.ContentLength];
            imageFile.InputStream.Read(vehicleImage.ImageBytes, 0, imageFile.ContentLength);
            db.VehicleImages.Add(vehicleImage);
            db.SaveChanges();
        }
    }
}