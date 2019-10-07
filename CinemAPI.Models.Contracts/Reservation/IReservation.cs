using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models.Contracts.Reservation
{
    public interface IReservation
    {
        long Id { get;}

        long ProjectionId { get; set; }

        DateTime StartDate { get; }

        string MovieName { get; }

        string CinemaName { get; }

        int RoomNumber { get; }

        byte Row { get; }

        byte Column { get; }

        bool IsCanceled { get; set; }

        bool IsBought { get; set; }
    }
}
