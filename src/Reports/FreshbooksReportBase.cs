using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vooban.FreshBooks.Project;
using Vooban.FreshBooks.Project.Models;
using Vooban.FreshBooks.Staff;
using Vooban.FreshBooks.Staff.Models;
using Vooban.FreshBooks.Task;
using Vooban.FreshBooks.Task.Models;
using Vooban.FreshBooks.TimeEntry;

namespace Vooban.FreshBooks.Reports
{
    public class FreshbooksReportBase
    {
        #region Security

        private readonly string _username;
        private readonly string _apiToken;

        #endregion

        #region Basic APIs

        private readonly Lazy<HastyAPI.FreshBooks.FreshBooks> _freshbooks;

        #endregion

        #region Cached items

        private IEnumerable<TaskModel> _freshbooksTasks = null;
        private IEnumerable<StaffModel> _freshbooksStaffMembers = null;
        private IEnumerable<ProjectModel> _freshbooksProjects = null;

        #endregion

        #region Constructors

        public FreshbooksReportBase(string username, string apiToken, bool warmupCaches = false)
        {
            _username = username;
            _apiToken = apiToken;

            _freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _apiToken));

            Initialize(warmupCaches);
        }

        public FreshbooksReportBase(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks, bool warmupCaches = false)
        {
            _freshbooks = freshbooks;

            Initialize(warmupCaches);
        }

        public FreshbooksReportBase(HastyAPI.FreshBooks.FreshBooks freshbooks, bool warmupCaches = false)
        {
            _freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => freshbooks);

            Initialize(warmupCaches);
        }

        #endregion

        #region Methods

        private void Initialize(bool warmupCaches = false)
        {
            TimeEntryService = new TimeEntryApi(_freshbooks);
            StaffMembersService = new StaffApi(_freshbooks);
            TasksService = new TaskApi(_freshbooks);
            ProjectsService = new ProjectApi(_freshbooks);

            if (warmupCaches)
                WarmupCaches();
        }

        protected void WarmupCaches()
        {
            if (_freshbooksTasks == null)
                _freshbooksTasks = TasksService.GetAllPages();

            if (_freshbooksStaffMembers == null)
                _freshbooksStaffMembers = StaffMembersService.GetAllPages();

            if (_freshbooksProjects == null)
                _freshbooksProjects = ProjectsService.GetAllPages();
        }

        #endregion

        #region Cached Elements

        protected IEnumerable<TaskModel> Tasks
        {
            get
            {
                if (_freshbooksTasks == null)
                    _freshbooksTasks = TasksService.GetAllPages();

                return _freshbooksTasks;
            }
        }

        protected IEnumerable<StaffModel> StaffMembers
        {
            get
            {
                if (_freshbooksStaffMembers == null)
                    _freshbooksStaffMembers = StaffMembersService.GetAllPages();

                return _freshbooksStaffMembers;
            }
        }

        protected IEnumerable<ProjectModel> Projects
        {
            get
            {
                if (_freshbooksProjects == null)
                    _freshbooksProjects = ProjectsService.GetAllPages();

                return _freshbooksProjects;
            }
        }

        #endregion

        #region Basic API Access

        protected TimeEntryApi TimeEntryService { get; private set; }

        protected StaffApi StaffMembersService { get; private set; }

        protected TaskApi TasksService { get; private set; }

        protected ProjectApi ProjectsService { get; private set; }

        #endregion
    }
}
