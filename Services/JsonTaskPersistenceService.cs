using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManager.Wpf.Models;

namespace TaskManager.Services
{
    /// <summary>
    /// Implements task persistence using JSON files in the user's AppData directory.
    /// </summary>
    public sealed class JsonTaskPersistenceService : ITaskPersistenceService
    {
        private readonly string _appDataFolder;
        private readonly JsonSerializerOptions _serializerOptions;

        /// <summary>
        /// Initializes a new instance, setting up the AppData folder and serializer options.
        /// </summary>
        public JsonTaskPersistenceService()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _appDataFolder = Path.Combine(appData, "TaskManager");
            Directory.CreateDirectory(_appDataFolder);

            _serializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        }

        /// <inheritdoc />
        public async Task SaveTasksAsync(IEnumerable<TaskItem> tasks, string filePath)
        {
            var fullPath = GetFullPath(filePath);

            try
            {
                using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
                await JsonSerializer.SerializeAsync(stream, tasks, _serializerOptions).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Log or handle exception as needed (for demo, rethrow)
                throw new IOException($"Error saving tasks to '{fullPath}': {ex.Message}", ex);
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TaskItem>> LoadTasksAsync(string filePath)
        {
            var fullPath = GetFullPath(filePath);

            if (!File.Exists(fullPath))
                return Array.Empty<TaskItem>();

            try
            {
                using var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
                var tasks = await JsonSerializer.DeserializeAsync<IEnumerable<TaskItem>>(stream, _serializerOptions).ConfigureAwait(false);
                return tasks ?? Array.Empty<TaskItem>();
            }
            catch (Exception ex)
            {
                // Log or handle exception as needed (for demo, return empty)
                return Array.Empty<TaskItem>();
            }
        }

        /// <summary>
        /// Gets the absolute path for the given file name within the AppData folder.
        /// </summary>
        private string GetFullPath(string fileName)
        {
            return Path.Combine(_appDataFolder, fileName);
        }
    }
}
