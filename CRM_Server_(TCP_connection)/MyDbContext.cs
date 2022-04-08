using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{
    internal class MyDbContext : DbContext
    {
        public MyDbContext() : base("DbConnectionString")
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }

    }
}
