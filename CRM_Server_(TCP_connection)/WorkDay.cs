using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{

    internal class WorkDay
    {
        private double WorkHours { get; }
        public DateTime WorkDate;

        public WorkDay (DateTime workdate, double workhours)
        {
            WorkHours = workhours; 
            WorkDate = workdate;
        }

        public double WorkHoursAtDay ()
        {
            return WorkHours;

        }

    }
}
