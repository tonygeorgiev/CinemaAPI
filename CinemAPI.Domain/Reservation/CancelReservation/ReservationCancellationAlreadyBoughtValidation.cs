using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models.Reservation;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Reservation.CancelReservation
{
    public class ReservationCancellationAlreadyBoughtValidation : IReservationCancellation
    {
        private readonly IReservationRepository reservationRepo;
        private readonly IReservationCancellation reservationCancellation;

        public ReservationCancellationAlreadyBoughtValidation(IReservationRepository reservationRepo, IReservationCancellation reservationCancellation)
        {
            this.reservationRepo = reservationRepo ?? throw new ArgumentNullException();
            this.reservationCancellation = reservationCancellation ?? throw new ArgumentNullException();
        }
        public ReservationCancellationSummary CancelReservation(long reservationId)
        {
            IReservation reservationTicket = this.reservationRepo.GetById((int)reservationId);
            if (reservationTicket != null)
            {
                if (reservationTicket.IsBought)
                {
                    return new ReservationCancellationSummary(false, $"Ticket with reservation id {reservationId} has already been bought.");
                }
            }

            return this.reservationCancellation.CancelReservation(reservationId);
        }
    }
}
