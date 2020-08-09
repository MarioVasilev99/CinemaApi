using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Services;
using CinemAPI.Models.Dtos.Projection;

namespace CinemAPI.Domain.Services
{
    public class ProjectionService : IProjectionService
    {
        private readonly IProjectionRepository projectionRepo;

        public ProjectionService(IProjectionRepository projectionRepo)
        {
            this.projectionRepo = projectionRepo;
        }

        public ProjectionSeatsInfoDto GetProjectionSeatsInfoById(long projectionId)
        {
            ProjectionSeatsInfoDto projection = projectionRepo.GetProjectionSeatsInfo(projectionId);

            if (projection == null)
            {
                return null;
            }

            return projection;
        }

        public void DecreaseAvailableSeatsCount(long projectionId)
        {
            projectionRepo.DecreaseAvailableSeats(projectionId);
        }

        public void IncreaseAvailableSeatsCount(long projectionId)
        {
            projectionRepo.IncreaseAvailableSeats(projectionId);
        }
    }
}
