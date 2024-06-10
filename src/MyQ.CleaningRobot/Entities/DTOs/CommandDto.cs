namespace MyQ.CleaningRobot.Entities.DTOs;

/// <summary>
/// Represents a data transfer object for a command.
/// </summary>
public record CommandDto
{
    /// <summary>
    /// Type of the command.
    /// </summary>
    public required CommandType CommandType { get; set; }

    /// <summary>
    /// Battery consumption of the command.
    /// </summary>
    public required int BatteryConsuption { get; set; }

    /// <summary>
    /// Degrees of rotation for the command.
    /// </summary>
    public required int DegreesOfRotation { get; set; }
}
