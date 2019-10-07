using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models.Contracts.Ticket
{
    public interface ITicketCreation
    {
        long ProjectionId { get; set; }

        DateTime StartDate { get; }

        string MovieName { get; }

        string CinemaName { get; }

        byte Row { get; set; }

        byte Column { get; set; }

        int RoomNumber { get; }
    }
}
