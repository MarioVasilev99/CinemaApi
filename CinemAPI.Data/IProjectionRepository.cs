using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Dtos.Projection;
using System;
using System.Collections.Generic;

namespace CinemAPI.Data
{
    public interface IProjectionRepository
    {
        IProjection Get(int movieId, int roomId, DateTime startDate);

        bool DoesProjectionExist(long projectionId);

        ProjectionSeatsInfoDto GetProjectionSeatsInfo(long projectionId);

        ProjectionStartDto GetProjectionStartDate(long projectionId);

        void Insert(IProjectionCreation projection);

        void DecreaseAvailableSeats(long projectionId);

        void IncreaseAvailableSeats(long projectionId);

        IEnumerable<IProjection> GetActiveProjections(int roomId);
    }
}