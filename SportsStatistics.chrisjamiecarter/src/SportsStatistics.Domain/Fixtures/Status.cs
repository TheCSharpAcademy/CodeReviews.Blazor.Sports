using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed class Status : Enumeration<Status>
{
    public static readonly Status Scheduled = new(0, nameof(Scheduled));
    public static readonly Status InProgress = new(1, nameof(InProgress));
    public static readonly Status Completed = new(2, nameof(Completed));

    private Status(int id, string name) : base(id, name) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Status"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private Status() { }
}
