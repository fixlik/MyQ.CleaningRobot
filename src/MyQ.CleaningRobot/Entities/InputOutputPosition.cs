namespace MyQ.CleaningRobot.Entities;

/// <summary>
/// Represents the input/output position of a cleaning robot.
/// </summary>
public record InputOutputPosition : PositionBase
{
    /// <summary>
    /// Facing direction of the cleaning robot.
    /// </summary>
    public required string Facing { get; set; }
}
