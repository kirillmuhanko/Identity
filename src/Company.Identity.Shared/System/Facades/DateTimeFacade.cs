using Company.Identity.Shared.System.Interfaces.Facades;

namespace Company.Identity.Shared.System.Facades;

public class DateTimeFacade : IDateTimeFacade
{
    public DateTime UtcNow => DateTime.UtcNow;
}