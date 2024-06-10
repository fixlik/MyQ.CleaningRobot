namespace MyQ.CleaningRobot.Entities;

/// <summary>
/// Represents the input file for the cleaning robot.
/// </summary>
public record InputFile
{
    /// <summary>
    /// Map of the cleaning area.
    /// </summary>
    public required IEnumerable<IEnumerable<string>> Map { get; set; }

    /// <summary>
    /// Starting position of the cleaning robot.
    /// </summary>
    public required InputOutputPosition Start { get; set; }

    /// <summary>
    /// List of commands for the cleaning robot.
    /// </summary>
    public required IEnumerable<string> Commands { get; set; }

    /// <summary>
    /// Battery level of the cleaning robot.
    /// </summary>
    public required int Battery { get; set; }
}
