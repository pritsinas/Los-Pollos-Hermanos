using LosPollosHermanos.Repositories;

namespace LosPollosHermanos.Persistance
{
    public interface IUnitOfWork
    {
        IShipmentRepository Shipments { get; }
        ITypeOfLoadRepository TypeOfLoads { get; }

        void Complete();
        void Dispose();
    }
}