using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Data
{
    public interface ITicketRepository
    {
        void Insert(ITicketCreation ticket);

        bool DoesTicketExist(long projectionId, int row, int col);
    }
}
