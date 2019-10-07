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
    public class AvailableSeatsAlreadyStartedProjectionValidation : IAvailableSeats 
    {
        private readonly IProjectionRepository projectionRepo;
        private readonly IAvailableSeats availableSeats;

        public AvailableSeatsAlreadyStartedProjectionValidation(IProjectionRepository projectionRepository, IAvailableSeats availableSeats)
        {
            this.projectionRepo = projectionRepository;
            this.availableSeats = availableSeats;
        }
        public AvailableSeatsSummary AvailableSeatsCheckById(long projectionId)
        {
            IProjection projection = projectionRepo.Get(projectionId);

            if (projection != null)
            {
                if (projection.StartDate <= DateTime.Now.Subtract(TimeSpan.FromMinutes(10)))
                {
                    return new AvailableSeatsSummary(false, "Can't reserve seats for projection which is starting in less than 10 minutes.");
                }
            }


            return availableSeats.AvailableSeatsCheckById(projectionId);
        }
    }
}

