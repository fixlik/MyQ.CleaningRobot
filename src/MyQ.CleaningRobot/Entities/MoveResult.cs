namespace MyQ.CleaningRobot.Entities;

/// <summary>
/// Represents the result of a move operation for a cleaning robot.
/// </summary>
public record MoveResult
{
    /// <summary>
    /// Value indicating whether the robot can continue moving.
    /// </summary>
    public bool CanContinue { get; set; }

    /// <summary>
    /// New position of the robot after the move operation.
    /// </summary>
    public Position NewPosition { get; set; }
}
