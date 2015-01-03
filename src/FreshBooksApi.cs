using System;
using Vooban.FreshBooks.Project;
using Vooban.FreshBooks.Staff;
using Vooban.FreshBooks.Task;
using Vooban.FreshBooks.TimeEntry;

namespace Vooban.FreshBooks
{
    public class FreshBooksApi : HastyAPI.FreshBooks.FreshBooks
    {
        private readonly Lazy<StaffApi> _staff;
        private readonly Lazy<ProjectApi> _project;
        private readonly Lazy<TaskApi> _task;
        private readonly Lazy<TimeEntryApi> _timeEntry;

        public FreshBooksApi(string username, string token) : base(username, token)
        {
            _staff = new Lazy<StaffApi>(() => new StaffApi(this));
            _project = new Lazy<ProjectApi>(() => new ProjectApi(this));
            _task = new Lazy<TaskApi>(() => new TaskApi(this));
            _timeEntry = new Lazy<TimeEntryApi>(() => new TimeEntryApi(this));        
        }

        public StaffApi Staff
        {
            get { return _staff.Value; }
        }

        public ProjectApi Projects
        {
            get { return _project.Value; }
        }

        public TaskApi Tasks
        {
            get { return _task.Value; }
        }

        public TimeEntryApi TimeEntries
        {
            get { return _timeEntry.Value; }
        }

        public static FreshBooksApi Build(string username, string token)
        {
            return new FreshBooksApi(username, token);
        }
    }
}
