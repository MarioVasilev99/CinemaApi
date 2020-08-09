using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Room;

namespace CinemAPI.Domain.NewProjection
{
    public class NewProjectionRoomSeatsValidation : INewProjection
    {
        private readonly IRoomRepository roomRepo;
        private readonly INewProjection newProj;

        public NewProjectionRoomSeatsValidation(IRoomRepository roomRepo, INewProjection newProj)
        {
            this.roomRepo = roomRepo;
            this.newProj = newProj;
        }

        public NewProjectionSummary New(IProjectionCreation proj)
        {
            IRoom room = roomRepo.GetById(proj.RoomId);

            if (room == null)
            {
                return new NewProjectionSummary(false, string.Format(ErrorMessagesHelper.ProjectionRoomNotExist, proj.RoomId));
            }

            int availableSeats = room.Rows * room.SeatsPerRow;
            if (availableSeats < 0)
            {
                return new NewProjectionSummary(false, ErrorMessagesHelper.ProjectionRoomNegativeSeats);
            }

            return newProj.New(proj);
        }
    }
}
