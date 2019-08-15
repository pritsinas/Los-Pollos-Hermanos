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
        [Authorize]
        public ActionResult MyShipments()
        {
            var userId = User.Identity.GetUserId();

            var myShipments = context.Shipments
                .Include(s => s.TypeOfLoad)
                .Where(s => s.DriverId == userId && !s.IsCancelled)
                .ToList();
            
            return View(myShipments);
        }

        //POST
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update (ShipmentFormViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.TypeOfLoads = context.TypeOfLoads.ToList();
                return View("ShipmentForm", viewModel);
            }

            var shipmentDb = context.Shipments.Single(s => s.Id == viewModel.Id);

            if (shipmentDb == null)
                return HttpNotFound();

            if (shipmentDb.DriverId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            shipmentDb.Modify(viewModel.GetDateTime(), viewModel.Location, viewModel.TypeOfLoad);

            context.SaveChanges();

            return RedirectToAction("MyShipments", "Shipments");
        }


        //GET
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var shipment = context.Shipments.Single(s => s.Id == id && s.DriverId == userId);

            var viewModel = new ShipmentFormViewModel()
            {
                Id = shipment.Id,
                Heading = "Edit Shipment",
                Location = shipment.Location,
                Date = shipment.DateTime.ToLongDateString(),
                Time = shipment.DateTime.ToString("HH:mm"),
                TypeOfLoad = shipment.TypeOfLoadId,
                TypeOfLoads = context.TypeOfLoads.ToList()             
            };

            return View("ShipmentForm", viewModel);
        }

        // POST:
        [HttpPost]
        [Authorize]
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
        [Authorize]
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