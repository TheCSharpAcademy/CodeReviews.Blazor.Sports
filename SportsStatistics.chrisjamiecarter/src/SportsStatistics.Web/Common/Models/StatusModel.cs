using SportsStatistics.Web.Common.Enums;

namespace SportsStatistics.Web.Common.Models;

public sealed class StatusModel
{
    public StatusModel() : this(string.Empty)
    {

    }

    public StatusModel(string? message) : this(message, StatusLevel.Primary)
    {

    }

    public StatusModel(string? message, string? levelString) : this(message)
    {
        if (Enum.TryParse<StatusLevel>(levelString, true, out var level))
        {
            Level = level;
        }
    }

    public StatusModel(string? message, StatusLevel level)
    {
        Message = message ?? string.Empty;
        Level = level;
    }

    public string Message { get; set; }
    public StatusLevel Level { get; set; }
}
