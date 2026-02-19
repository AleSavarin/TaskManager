using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Wpf.Models;

namespace TaskManager.Services
{
    /// <summary>
    /// Defines methods for persisting and retrieving TaskItem objects in JSON format.
    /// </summary>
    public interface ITaskPersistenceService
    {
        /// <summary>
        /// Saves a collection of tasks to a JSON file asynchronously.
        /// </summary>
        /// <param name="tasks">The collection of tasks to save.</param>
        /// <param name="filePath">The file path where the tasks will be saved.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SaveTasksAsync(IEnumerable<TaskItem> tasks, string filePath);

        /// <summary>
        /// Loads a collection of tasks from a JSON file asynchronously.
        /// </summary>
        /// <param name="filePath">The file path from which the tasks will be loaded.</param>
        /// <returns>A task representing the asynchronous operation, containing the loaded tasks.</returns>
        Task<IEnumerable<TaskItem>> LoadTasksAsync(string filePath);
    }
}