using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.Wpf.Models;
using TaskManager.Commands;
using TaskManager.Services;

namespace TaskManager.ViewModels
{
    /// <summary>
    /// ViewModel principal para la gestión de tareas.
    /// Permite cargar, agregar y eliminar tareas, y notifica cambios a la UI.
    /// </summary>
    public sealed class MainViewModel : BaseViewModel
    {
        private readonly ITaskPersistenceService _persistenceService;
        private string _newTaskTitle = string.Empty;
        private const string TasksFileName = "tasks.json";

        /// <summary>
        /// Colección observable de tareas para la UI.
        /// </summary>
        public ObservableCollection<TaskItem> Tasks { get; } = new();

        /// <summary>
        /// Título de la nueva tarea a crear.
        /// </summary>
        public string NewTaskTitle
        {
            get => _newTaskTitle;
            set => SetProperty(ref _newTaskTitle, value);
        }

        /// <summary>
        /// Comando para agregar una nueva tarea.
        /// </summary>
        public ICommand AddTaskCommand { get; }

        /// <summary>
        /// Comando para eliminar una tarea existente.
        /// </summary>
        public ICommand RemoveTaskCommand { get; }

        /// <summary>
        /// Inicializa el ViewModel e inyecta el servicio de persistencia.
        /// </summary>
        /// <param name="persistenceService">Servicio de persistencia de tareas.</param>
        public MainViewModel(ITaskPersistenceService persistenceService)
        {
            _persistenceService = persistenceService ?? throw new ArgumentNullException(nameof(persistenceService));

            AddTaskCommand = new RelayCommand(
                _ => AddTask(),
                _ => !string.IsNullOrWhiteSpace(NewTaskTitle)
            );

            RemoveTaskCommand = new RelayCommand(
                task => RemoveTask(task as TaskItem),
                task => task is TaskItem
            );
        }

        /// <summary>
        /// Carga las tareas desde el almacenamiento persistente de forma asíncrona.
        /// </summary>
        public async Task InitializeAsync()
        {
            var loadedTasks = await _persistenceService.LoadTasksAsync(TasksFileName).ConfigureAwait(false);
            Tasks.Clear();
            foreach (var task in loadedTasks)
            {
                Tasks.Add(task);
            }
        }

        /// <summary>
        /// Agrega una nueva tarea a la colección y la guarda.
        /// </summary>
        private async void AddTask()
        {
            var task = new TaskItem
            {
                Title = NewTaskTitle,
                IsCompleted = false
            };
            Tasks.Add(task);
            NewTaskTitle = string.Empty;
            await SaveTasksAsync();
        }

        /// <summary>
        /// Elimina una tarea de la colección y actualiza el almacenamiento.
        /// </summary>
        /// <param name="task">La tarea a eliminar.</param>
        private async void RemoveTask(TaskItem? task)
        {
            if (task is null) return;
            Tasks.Remove(task);
            await SaveTasksAsync();
        }

        /// <summary>
        /// Guarda la colección de tareas en el almacenamiento persistente.
        /// </summary>
        private async Task SaveTasksAsync()
        {
            await _persistenceService.SaveTasksAsync(Tasks, TasksFileName).ConfigureAwait(false);
        }
    }
}
