using CinemAPI.Domain.Contracts.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts
{
    public interface IReservationCancellation
    {
        ReservationCancellationSummary CancelReservation(long reservationId);
    }
}
