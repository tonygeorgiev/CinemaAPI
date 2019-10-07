using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;

namespace CinemAPI.Data
{
    public interface IProjectionRepository
    {
        IProjection Get(int movieId, int roomId, DateTime startDate);

        IProjection Get(long id);

        void Insert(IProjectionCreation projection);

        void Update(Projection entity);

        IEnumerable<IProjection> GetActiveProjections(int roomId);

        void Save();
    }
}