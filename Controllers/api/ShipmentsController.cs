using LosPollosHermanos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using LosPollosHermanos.Persistance;

namespace LosPollosHermanos.Controllers.api
{
    
    public class ShipmentsController : ApiController
    {
        //private readonly ApplicationDbContext context;
        private readonly IUnitOfWork unitOfWork;

        public ShipmentsController(IUnitOfWork unitOfWork)
        {
            //context = new ApplicationDbContext();
            this.unitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
        }

        //api/shipments/id
        [HttpDelete]
        [Authorize(Roles = RoleName.Driver)]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();

            //var shipment = context.Shipments.Single(s => s.Id == id && s.DriverId == userId);
            var shipment = unitOfWork.Shipments.EditShipment(id, userId);

            if (shipment.IsCancelled)
                return NotFound();

            shipment.Cancel();

            //context.SaveChanges();
            unitOfWork.Complete();

            return Ok();
        }
    }
}
