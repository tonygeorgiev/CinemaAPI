using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.Models.Reservation
{
    public class ReservationCancellationSummary
    {
        public ReservationCancellationSummary(bool reservationIsCanceled)
        {
            this.ReservationIsCanceled = reservationIsCanceled;
        }

        public ReservationCancellationSummary(bool status, string msg)
            : this(status)
        {
            this.Message = msg;
        }

        public string Message { get; set; }

        public bool ReservationIsCanceled { get; set; }
    }
}
