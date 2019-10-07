using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models.Reservation;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Reservation.CancelReservation
{
    public class ReservationCancellation : IReservationCancellation
    {
        private readonly IReservationRepository reservationRepo;
        private readonly IProjectionRepository projectionRepo;

        public ReservationCancellation(IReservationRepository reservationRepo, IProjectionRepository projectionRepo)
        {
            this.reservationRepo = reservationRepo ?? throw new ArgumentNullException();
            this.projectionRepo = projectionRepo ?? throw new ArgumentNullException();
        }
    
        public ReservationCancellationSummary CancelReservation(long reservationId)
        {
            IReservation reservationTicket = this.reservationRepo.GetById((int)reservationId);
            if (reservationTicket != null)
            {
                IProjection projection = projectionRepo.Get(reservationTicket.ProjectionId);
                if (projection != null)
                {
                    projection.AvailableSeatsCount += 1;
                    reservationTicket.IsCanceled = true;

                    this.reservationRepo.Save();

                    return new ReservationCancellationSummary(true, "Reservation has been successfully canceled.");
                }

                return new ReservationCancellationSummary(false, $"Projection with id {reservationTicket.ProjectionId} does not exist");
            }

            return new ReservationCancellationSummary(false, $"Reservation with id {reservationId} does not exist");
        }
    }
}
