namespace MyQ.CleaningRobot.Entities.DTOs;

/// <summary>
/// Represents the data transfer object for an input file.
/// </summary>
public record InputFileDto
{
    /// <summary>
    /// Map information.
    /// </summary>
    public required MapDto Map { get; set; }

    /// <summary>
    /// Starting position.
    /// </summary>
    public required InputOutputPositionDto Start { get; set; }

    /// <summary>
    /// List of commands.
    /// </summary>
    public required IEnumerable<CommandDto> Commands { get; set; }

    /// <summary>
    /// Battery level.
    /// </summary>
    public required int Battery { get; set; }
}
