using LosPollosHermanos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LosPollosHermanos.Repositories
{
    public class TypeOfLoadRepository : ITypeOfLoadRepository
    {
        private readonly ApplicationDbContext _context;

        public TypeOfLoadRepository(ApplicationDbContext context)
        {
            _context = context;
        }
            
        public IEnumerable<TypeOfLoad> GetTypesOfLoad()
        {
            return _context.TypeOfLoads.ToList();
        }
    }
}