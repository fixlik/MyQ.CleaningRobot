using MyQ.CleaningRobot.Entities;
using MyQ.CleaningRobot.Entities.DTOs;
using MyQ.CleaningRobot.Helpers;

namespace MyQ.CleaningRobot.Extensions;

/// <summary>
/// Extension methods for <see cref="InputOutputPosition"/>.
/// </summary>
public static class InputOutputPositionExtensions
{
    /// <summary>
    /// Converts an InputOutputPosition object to an InputOutputPositionDto object.
    /// </summary>
    /// <param name="start">The InputOutputPosition object to convert.</param>
    /// <returns>The converted InputOutputPositionDto object.</returns>
    public static InputOutputPositionDto ToDto(this InputOutputPosition start)
    {
        return new InputOutputPositionDto
        {
            X = start.X,
            Y = start.Y,
            Facing = CardinalDirectionHelper.MapCardinalDirection(start.Facing)
        };
    }
}
