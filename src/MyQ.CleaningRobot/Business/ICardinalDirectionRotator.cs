using MyQ.CleaningRobot.Entities;

namespace MyQ.CleaningRobot.Business;

/// <summary>
/// Represents an interface for rotating cardinal directions.
/// </summary>
public interface ICardinalDirectionRotator
{
    /// <summary>
    /// Rotates the start direction by the specified number of degrees.
    /// </summary>
    /// <param name="startDirection">The starting cardinal direction.</param>
    /// <param name="degrees">The number of degrees to rotate.</param>
    /// <returns>The new cardinal direction after rotation.</returns>
    public CardinalDirection Rotate(CardinalDirection startDirection, int degrees);
}
