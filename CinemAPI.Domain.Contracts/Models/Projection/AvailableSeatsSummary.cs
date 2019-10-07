using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.Models.Projection
{
    public class AvailableSeatsSummary
    {
        public AvailableSeatsSummary(bool seatsAreAvailable)
        {
            this.SeatsAreAvailable = seatsAreAvailable;
        }

        public AvailableSeatsSummary(bool status, string msg)
            : this(status)
        {
            this.Message = msg;
        }

        public string Message { get; set; }

        public bool SeatsAreAvailable { get; set; }

        public int AvailableSeats { get; set; }
    }
}
