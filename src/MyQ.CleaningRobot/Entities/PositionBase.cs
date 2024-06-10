namespace MyQ.CleaningRobot.Entities;

/// <summary>
/// Represents the base class for a position in a cleaning robot system.
/// </summary>
public record PositionBase
{
    /// <summary>
    /// X coordinate of the position.
    /// </summary>
    public required int X { get; set; }

    /// <summary>
    /// Y coordinate of the position.
    /// </summary>
    public required int Y { get; set; }
}
