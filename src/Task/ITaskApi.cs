using FreshBooks.Api.Task.Models;

namespace FreshBooks.Api.Task
{
    public interface ITaskApi
        : IFullApi<TaskModel, TaskFilter>
    {
    
    }
}