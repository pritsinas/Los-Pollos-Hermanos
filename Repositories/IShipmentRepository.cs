using System.Collections.Generic;
using LosPollosHermanos.Models;

namespace LosPollosHermanos.Repositories
{
    public interface IShipmentRepository
    {
        void Add(Shipment shipment);
        Shipment EditShipment(int shipmentId, string userId);
        IEnumerable<Shipment> GetMyShipments(string userId);
        Shipment GetShipment(int shipmentId);
    }
}