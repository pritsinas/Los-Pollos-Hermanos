using LosPollosHermanos.Models;
using LosPollosHermanos.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LosPollosHermanos.Persistance;

namespace LosPollosHermanos.Controllers
{
    public class ShipmentsController : Controller
    {
        //private readonly ApplicationDbContext context;

        private readonly IUnitOfWork unitOfWork;

        public ShipmentsController(IUnitOfWork unitOfWork)
        {
            //context = new ApplicationDbContext();
            this.unitOfWork = unitOfWork;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    context.Dispose();
        //}

        //GET:
        [Authorize(Roles = RoleName.Driver)]
        public ActionResult MyShipments()
        {
            var userId = User.Identity.GetUserId();

            //var myShipments = context.Shipments
            //    .Include(s => s.TypeOfLoad)
            //    .Where(s => s.DriverId == userId && !s.IsCancelled)
            //    .ToList();

            var myShipments = unitOfWork.Shipments.GetMyShipments(userId);

            return View(myShipments);
        }

        //POST
        [HttpPost]
        [Authorize(Roles = RoleName.Driver)]
        [ValidateAntiForgeryToken]
        public ActionResult Update (ShipmentFormViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                //viewModel.TypeOfLoads = context.TypeOfLoads.ToList();
                viewModel.TypeOfLoads = unitOfWork.TypeOfLoads.GetTypesOfLoad();
                return View("ShipmentForm", viewModel);
            }

            //var shipmentDb = context.Shipments.Single(s => s.Id == viewModel.Id);
            var shipmentDb = unitOfWork.Shipments.GetShipment(viewModel.Id);

            if (shipmentDb == null)
                return HttpNotFound();

            if (shipmentDb.DriverId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            shipmentDb.Modify(viewModel.GetDateTime(), viewModel.Location, viewModel.TypeOfLoad);

            //context.SaveChanges();
            unitOfWork.Complete();

            return RedirectToAction("MyShipments", "Shipments");
        }


        //GET
        [Authorize(Roles = RoleName.Driver)]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            //var shipment = context.Shipments.Single(s => s.Id == id && s.DriverId == userId);
            var shipment = unitOfWork.Shipments.EditShipment(id, userId);

            var viewModel = new ShipmentFormViewModel()
            {
                Id = shipment.Id,
                Heading = "Edit Shipment",
                Location = shipment.Location,
                Date = shipment.DateTime.ToLongDateString(),
                Time = shipment.DateTime.ToString("HH:mm"),
                TypeOfLoad = shipment.TypeOfLoadId,
                //TypeOfLoads = context.TypeOfLoads.ToList() 
                TypeOfLoads = unitOfWork.TypeOfLoads.GetTypesOfLoad()
            };

            return View("ShipmentForm", viewModel);
        }

        // POST:
        [HttpPost]
        [Authorize(Roles = RoleName.Driver)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShipmentFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                //viewModel.TypeOfLoads = context.TypeOfLoads.ToList();
                viewModel.TypeOfLoads = unitOfWork.TypeOfLoads.GetTypesOfLoad();

                return View("ShipmentForm", viewModel);
            }

            var shipment = new Shipment()
            {
                DriverId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                TypeOfLoadId = viewModel.TypeOfLoad,
                Location = viewModel.Location
            };

            //context.Shipments.Add(shipment);
            //context.SaveChanges();
            unitOfWork.Shipments.Add(shipment);
            unitOfWork.Complete();

            return RedirectToAction("MyShipments", "Shipments");
        }

        // GET:
        [Authorize(Roles = RoleName.Driver)]
        public ActionResult Create()
        {
            var viewModel = new ShipmentFormViewModel
            {
                Heading = "Add Shipment",
                //TypeOfLoads = context.TypeOfLoads.ToList()
                TypeOfLoads = unitOfWork.TypeOfLoads.GetTypesOfLoad()
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