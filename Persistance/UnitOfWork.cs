using LosPollosHermanos.Models;
using LosPollosHermanos.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LosPollosHermanos.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IShipmentRepository Shipments { get; private set; }
        public ITypeOfLoadRepository TypeOfLoads { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Shipments = new ShipmentRepository(context);
            TypeOfLoads = new TypeOfLoadRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}