using GolfAPI.DataLayer.ADL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.DataLayer.DataAccess
{
    public class BaseRepository
    {
        protected readonly GolfDatabaseContext _context;
        public BaseRepository(GolfDatabaseContext context)
        {
            _context = context;
        }
    }
}
