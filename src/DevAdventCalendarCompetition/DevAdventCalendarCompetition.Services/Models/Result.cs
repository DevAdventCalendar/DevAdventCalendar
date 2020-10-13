namespace DevAdventCalendarCompetition.Services.Models
{
    public enum OperationalResultStatus
    {
        Success,
        CalendarFailure,
        EventsFailure
    }

    public class OperationalResult
    {
        private OperationalResult(OperationalResultStatus status)
        {
            this.Status = status;
        }

        public OperationalResultStatus Status { get; }

        public static OperationalResult Success() => new OperationalResult(OperationalResultStatus.Success);

        public static OperationalResult Failure(OperationalResultStatus status) => new OperationalResult(status);
    }
}
