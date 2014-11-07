using Vooban.FreshBooks.Project.Models;

namespace Vooban.FreshBooks.Project
{
    public interface IProjectApi
        : IFullApi<ProjectModel, ProjectFilter>
    {
    
    }
}