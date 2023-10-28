using fiap.application.Interfaces;

namespace fiap.infrastructure.Providers
{
    public class DateTimeProvider : IDatetimeProvider
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}
