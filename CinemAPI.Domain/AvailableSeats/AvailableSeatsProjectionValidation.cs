using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models.Projection;
using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.AvailableSeats
{
    public class AvailableSeatsProjectionValidation : IAvailableSeats
    {
        private readonly IProjectionRepository projectionRepo;
        private readonly IAvailableSeats availableSeats;

        public AvailableSeatsProjectionValidation(IProjectionRepository projectionRepository, IAvailableSeats availableSeats)
        {
            this.projectionRepo = projectionRepository;
            this.availableSeats = availableSeats;
        }
        public AvailableSeatsSummary AvailableSeatsCheckById(long projectionId)
        {
            IProjection projection = projectionRepo.Get(projectionId);

            if (projection == null)
            {
                return new AvailableSeatsSummary(false, $"Projection with id {projectionId} does not exist");
            }

            return availableSeats.AvailableSeatsCheckById(projectionId);
        }
    }
}







