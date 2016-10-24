using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoScout.Models
{
    public class Dealership
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int DealershipLicenseNumber { get; set; }
        public bool Certified { get; set; }
        public bool Active { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Icon { get; set; }
        public byte[] ProfileBackgroundImage { get; set; }
        public ICollection<Vehicle> Vehicles;
        public virtual AutoScoutIdentityUser AutoScoutIdentityUser { get; set; }
        public string AutoScoutIdentityUserId { get; set; }

    }
}