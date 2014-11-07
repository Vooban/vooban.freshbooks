using Vooban.FreshBooks.Task.Models;

namespace Vooban.FreshBooks.Task
{
    public interface ITaskApi
        : IFullApi<TaskModel, TaskFilter>
    {
    
    }
}