using MyQ.CleaningRobot.Entities;

namespace MyQ.CleaningRobot.Business;

/// <summary>
/// Rotates the cardinal direction
/// </summary>
public class CardinalDirectionRotator : ICardinalDirectionRotator
{
    /// <summary>
    /// Rotates the start direction by the specified number of degrees.
    /// </summary>
    /// <param name="startDirection">The starting cardinal direction.</param>
    /// <param name="degrees">The number of degrees to rotate.</param>
    /// <returns>The new cardinal direction after rotation.</returns>
    /// <exception cref="ArgumentException">Thrown when degrees is not a multiple of 90.</exception>
    public CardinalDirection Rotate(CardinalDirection startDirection, int degrees)
    {
        if (degrees % 90 != 0)
        {
            throw new ArgumentException("Only increments of 90 are supported");
        }

        var steps = degrees / 90;

        var directionCount = Enum.GetValues(typeof(CardinalDirection)).Length;
        var startValue = (int)startDirection;

        var newValue = (startValue + steps + directionCount) % directionCount;

        var newDirection = (CardinalDirection)newValue;

        return newDirection;
    }
}
