using FreshBooks.Api.Project.Models;

namespace FreshBooks.Api.Project
{
    public interface IProjectApi
        : IFullApi<ProjectModel, ProjectFilter>
    {
    
    }
}