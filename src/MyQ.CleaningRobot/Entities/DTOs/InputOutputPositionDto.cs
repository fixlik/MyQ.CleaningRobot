namespace MyQ.CleaningRobot.Entities.DTOs;

/// <summary>
/// Represents the data transfer object for input and output positions.
/// </summary>
public record InputOutputPositionDto : PositionBase
{
    /// <summary>
    /// Facing direction of the position.
    /// </summary>
    public required CardinalDirection Facing { get; set; }
}
