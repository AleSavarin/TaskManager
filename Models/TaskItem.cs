namespace TaskManager.Wpf.Models;

/// <summary>
/// Represents a single task in the system.
/// </summary>
public sealed class TaskItem
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
