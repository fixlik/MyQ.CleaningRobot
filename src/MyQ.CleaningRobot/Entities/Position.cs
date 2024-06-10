namespace MyQ.CleaningRobot.Entities;

/// <summary>
/// Represents the position of a cleaning robot.
/// </summary>
public record Position : PositionBase
{
    /// <summary>
    /// Facing direction of the robot.
    /// </summary>
    public required CardinalDirection Facing { get; set; }

    /// <summary>
    /// Type of the cell at the position.
    /// </summary>
    public CellType? CellType { get; set; }
}
