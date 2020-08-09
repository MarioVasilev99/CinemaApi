using CinemAPI.Models.Dtos.Projection;

namespace CinemAPI.Domain.Contracts.Services
{
    public interface IProjectionService
    {
        ProjectionSeatsInfoDto GetProjectionSeatsInfoById(long projectionId);

        void DecreaseAvailableSeatsCount(long projectionId);

        void IncreaseAvailableSeatsCount(long projectionId);
    }
}
