using LosPollosHermanos.Models;
using LosPollosHermanos.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace LosPollosHermanos.Controllers
{
    public class ShipmentsController : Controller
    {
        private readonly ApplicationDbContext context;

        public ShipmentsController()
        {
            context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        //GET:
        public ActionResult MyShipments()
        {
            var userId = User.Identity.GetUserId();

            var myShipment = context.Shipments.Include(s => s.TypeOfLoad).Where(s => s.DriverId == userId);

            return View(myShipment);
        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShipmentFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.TypeOfLoads = context.TypeOfLoads.ToList();

                return View("ShipmentForm", viewModel);
            }

            var shipment = new Shipment()
            {
                DriverId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                TypeOfLoadId = viewModel.TypeOfLoad,
                Location = viewModel.Location
            };

            context.Shipments.Add(shipment);
            context.SaveChanges();

            return RedirectToAction("MyShipments", "Shipments");
        }

        // GET:
        public ActionResult Create()
        {
            var viewModel = new ShipmentFormViewModel
            {
                Heading = "Add Shipment",
                TypeOfLoads = context.TypeOfLoads.ToList()
            };

            return View("ShipmentForm", viewModel);
        }

        // GET: Shipments
        public ActionResult Index()
        {
            return View();
        }

    }
}