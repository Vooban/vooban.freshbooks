using Vooban.FreshBooks.TimeEntry.Models;

namespace Vooban.FreshBooks.TimeEntry
{
    public interface ITimeEntryApi
        : IFullApi<TimeEntryModel, TimeEntryFilter>
    {
    
    }
}