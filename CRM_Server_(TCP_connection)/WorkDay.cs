using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{

    public class WorkDay
    {
        public int Id { get; set; }
        public double WorkHours { get; set; }
        public DateTime WorkDate { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public WorkDay()
        {
        }
        public WorkDay (DateTime workdate, double workhours, int employeeid)
        {
            WorkHours = workhours; 
            WorkDate = workdate;
            EmployeeId = employeeid;
        }
     
    }
}
