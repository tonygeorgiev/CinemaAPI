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
    public class AvailableSeatsCheck : IAvailableSeats
    {
        private readonly IProjectionRepository projectionsRepo;

        public AvailableSeatsCheck(IProjectionRepository projectionsRepo)
        {
            this.projectionsRepo = projectionsRepo;
        }

        public AvailableSeatsSummary AvailableSeatsCheckById(long projectionId)
        {
            IProjection projection = this.projectionsRepo.Get(projectionId);

            return new AvailableSeatsSummary(true) { AvailableSeats = projection.AvailableSeatsCount };
        }
    }
}
