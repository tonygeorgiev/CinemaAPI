using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models.Contracts.Reservation
{
    public interface IReservationCreation
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
