using MyQ.CleaningRobot.Entities;
using MyQ.CleaningRobot.Entities.DTOs;

namespace MyQ.CleaningRobot.Business;

/// <summary>
/// Represents the interface for a cleaning robot.
/// </summary>
internal interface ICleaningRobot
{
    /// <summary>
    /// Cleans the map based on the given input file.
    /// </summary>
    /// <param name="inputFile">The input file containing the map and commands.</param>
    /// <returns>The output file containing the visited cells, cleaned cells, final position, and battery level.</returns>
    public OutputFile Clean(InputFileDto inputFile);
}
