using MyQ.CleaningRobot.Entities;
using MyQ.CleaningRobot.Entities.DTOs;
using MyQ.CleaningRobot.Helpers;

namespace MyQ.CleaningRobot.Extensions;

/// <summary>
/// Extension methods for <see cref="InputFile"/>.
/// </summary>
public static class InputFileExtensions
{
    /// <summary>
    /// Converts an InputFile object to an InputFileDto object.
    /// </summary>
    /// <param name="inputFile">The InputFile object to convert.</param>
    /// <returns>The converted InputFileDto object.</returns>
    public static InputFileDto ToDto(this InputFile inputFile)
    {
        return new InputFileDto
        {
            Map = MapHelper.CreateMapDto(inputFile.Map),
            Start = inputFile.Start.ToDto(),
            Commands = inputFile.Commands.Select(CommandHelper.CreateCommandDto),
            Battery = 80
        };
    }
}
