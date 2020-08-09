using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.Services;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Input.Ticket;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class TicketController : ApiController
    {
        private readonly INewTicket newTicket;
        private readonly IProjectionService projectionService;
        private readonly IReservationService reservationService;
        private readonly IReservationRepository reservationRepo;
        private readonly ITicketRepository ticketRepo;

        public TicketController(
            INewTicket newTicket,
            IProjectionService projectionService,
            IReservationService reservationService,
            IReservationRepository reservationRepo,
            ITicketRepository ticketRepo)
        {
            this.newTicket = newTicket;
            this.projectionService = projectionService;
            this.reservationService = reservationService;
            this.reservationRepo = reservationRepo;
            this.ticketRepo = ticketRepo;
        }

        [HttpPost]
        public IHttpActionResult Index(TicketCreationModel model)
        {
            NewTicketSummary summary = newTicket.New(new Ticket(model.ProjectionId, model.Row, model.Col));

            if (summary.IsCreated)
            {
                projectionService.DecreaseAvailableSeatsCount(model.ProjectionId);
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        [Route(EndpointsRoutesHelper.TicketWithReservationRoute)]
        [HttpPost]
        public IHttpActionResult TicketWithReservation(TicketWithReservationModel model)
        {
            ReservationValidationSummary validationSummary = reservationService.ValidateReservation(model.ReservationId);

            if (validationSummary.IsValid)
            {
                IReservation reservation = reservationRepo.GetReservationById(model.ReservationId);
                ticketRepo.Insert(new Ticket(reservation.ProjectionId, reservation.Row, reservation.Col));
                projectionService.DecreaseAvailableSeatsCount(reservation.ProjectionId);

                return Ok();
            }
            else
            {
                return BadRequest(validationSummary.Message);
            }
        }
    }
}