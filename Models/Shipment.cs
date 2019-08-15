using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LosPollosHermanos.Models
{
    public class Shipment
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public string Location { get; set; }

        public bool IsCancelled { get; private set; }

        [Display(Name = "Load Type")]
        public byte TypeOfLoadId { get; set; } 

        public TypeOfLoad TypeOfLoad { get; set; }

        public string DriverId { get; set; }

        public ApplicationUser Driver { get; set; }

        public void Modify(DateTime dateTime, string location, byte typeOfLoad)
        {
            Location = location;
            DateTime = dateTime;
            TypeOfLoadId = typeOfLoad;

        }

        public void Cancel()
        {
            IsCancelled = true;
        }
    }
}