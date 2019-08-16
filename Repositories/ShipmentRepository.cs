using LosPollosHermanos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LosPollosHermanos.Repositories
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly ApplicationDbContext _context;

        public ShipmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Shipment> GetMyShipments(string userId)
        {
            return _context.Shipments
                .Include(s => s.TypeOfLoad)
                .Where(s => s.DriverId == userId && !s.IsCancelled)
                .ToList();

        }

        public Shipment EditShipment(int shipmentId, string userId)
        {
            return _context.Shipments.Single(s => s.Id == shipmentId && s.DriverId == userId);
        }

        public Shipment GetShipment(int shipmentId)
        {
            return _context.Shipments.Single(s => s.Id == shipmentId);
        }

        public void Add(Shipment shipment)
        {
            _context.Shipments.Add(shipment);
        }
    }
}