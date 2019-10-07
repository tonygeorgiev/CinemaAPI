using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemAPI.Models.Input.Reservation
{
    public class SeatReservationModel
    {
        public long ProjectionId { get; set; }

        public byte Row { get; set; }

        public byte Column { get; set; }
    }
}