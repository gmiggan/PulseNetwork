using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PulseNetwork
{
    public class PulseDbContext : DbContext
    {
        public PulseDbContext(): base("name=DatabaseConnection")
        {
            
        }
    }
}