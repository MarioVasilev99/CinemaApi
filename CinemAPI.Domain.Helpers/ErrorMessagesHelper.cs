namespace CinemAPI.Domain.Helpers
{
    public static class ErrorMessagesHelper
    {
        //Cinema
        public const string CinemaExists = "Cinema already exists.";

        //Movie
        public const string MovieExists = "Movie already exists.";

        //Projection
        public const string ProjectionStartedOrFinished = "The projection has already finished or started.";
        public const string ProjectionMovieWithIdNotExist = "Movie with id {0} does not exist.";
        public const string ProjectionOverlapsWithNext = "Projection overlaps with next one: {0} at {1}.";
        public const string ProjectionOverlapsWithPrevious = "Projection overlaps with previous one: {0} at {1}.";
        public const string ProjectionRoomNotExist = "Room with id {0} does not exist.";
        public const string ProjectionRoomNegativeSeats = "Projection room can't have less than 0 available seats.";
        public const string ProjectionExists = "Projection already exists.";

        //Room
        public const string RoomExists = "Room already exists.";

        //Reservation
        public const string ReservationNotFound = "Reservation not found.";
        public const string ReservationCancelled = "Reservation already cancelled.";
        public const string ReservationColumnNotValid = "Seat number {0} is not valid.";
        public const string ReservationProjectionFinished = "Projection has already finished.";
        public const string ReservationProjectionStartSoon = "Projection starts in less than {0} minutes.";
        public const string ReservationProjectionStarted = "Projection has already started.";
        public const string ReservationProjectionNotExist = "Projection with id {0} does not exist.";
        public const string ReservationRowNumberNotValid = "Row number {0} is not valid.";
        public const string ReservationSeatAlreadyReserved = "Seat ({0}, {1}) is already reserved.";

        //Ticket
        public const string TicketProjectionNotExist = "Projection with id {0} does not exist.";
        public const string TickerProjectionPassed = "Projection has already started or finished. Ticket can't be bought!";
        public const string TicketSeatNotExisting = "Seat Not Existing. Projection Room has {0} rows and {1} seats per row.";
        public const string TicketSeatAlreadyTaken = "Seat ({0}, {1}) has already been reserved or bought.";
    }
}
