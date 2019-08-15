using LosPollosHermanos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace LosPollosHermanos.Controllers.api
{
    [Authorize]
    public class ShipmentsController : ApiController
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

        //api/shipments/id
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();

            var shipment = context.Shipments.Single(s => s.Id == id && s.DriverId == userId);

            if (shipment.IsCancelled)
                return NotFound();

            shipment.Cancel();

            context.SaveChanges();

            return Ok();
        }
    }
}
