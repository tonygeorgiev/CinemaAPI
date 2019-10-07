using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemAPI.Models.Input.Ticket
{
    public class TicketWithoutReservationModel
    {
        public long ProjectionId { get; set; }

        public byte Row { get; set; }

        public byte Column { get; set; }
    }
}