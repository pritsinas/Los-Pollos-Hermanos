using LosPollosHermanos.Controllers;
using LosPollosHermanos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace LosPollosHermanos.ViewModels
{
    public class ShipmentFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public byte TypeOfLoad { get; set; }

        public IEnumerable<TypeOfLoad> TypeOfLoads { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<ShipmentsController, ActionResult>> create = (c => c.Create());
             
                return (create.Body as MethodCallExpression).Method.Name;
            }
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }

    }
}