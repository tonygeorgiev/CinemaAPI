using CinemAPI.Domain.Contracts.Models.Projection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts
{
    public interface IAvailableSeats
    {
        AvailableSeatsSummary AvailableSeatsCheckById(long projectionId);
    }
}
