using CinemAPI.Models.Contracts.Room;
using CinemAPI.Models.Dtos.Room;

namespace CinemAPI.Data
{
    public interface IRoomRepository
    {
        IRoom GetById(int id);

        IRoom GetByCinemaAndNumber(int cinemaId, int number);

        RoomSeatsRowsDto GetRoomSeatsRowsInfo(long projectionId);

        void Insert(IRoomCreation room);
    }
}