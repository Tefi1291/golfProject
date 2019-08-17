using GolfAPI.DataLayer.ADL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.DataLayer.DataAccess
{
    /// <summary>
    /// Base class for data repositories.
    /// Every repository must inherit it
    /// </summary>
    public class BaseRepository
    {
        /// <summary>
        /// Context for database conection
        /// </summary>
        protected readonly GolfDatabaseContext _context;
        public BaseRepository(GolfDatabaseContext context)
        {
            _context = context;
        }
    }
}
