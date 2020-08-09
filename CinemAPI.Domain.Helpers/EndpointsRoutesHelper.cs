namespace CinemAPI.Domain.Helpers
{
    public static class EndpointsRoutesHelper
    {
        //Projection Controller
        public const string GetAvailableSeatsRoute = "api/projection/seats/{projectionId}";

        //Reservation Controller
        public const string CancelReservationRoute = "api/reservation/cancel/{reservationId}";

        //Tickets Controller
        public const string TicketWithReservationRoute = "api/ticket/reservation";
    }
}
