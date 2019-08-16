using System.Collections.Generic;
using LosPollosHermanos.Models;

namespace LosPollosHermanos.Repositories
{
    public interface ITypeOfLoadRepository
    {
        IEnumerable<TypeOfLoad> GetTypesOfLoad();
    }
}