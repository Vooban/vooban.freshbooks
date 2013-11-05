using FreshBooks.Api.TimeEntry.Models;

namespace FreshBooks.Api.TimeEntry
{
    public interface ITimeEntryApi
        : IFullApi<TimeEntryModel, TimeEntryFilter>
    {
    
    }
}