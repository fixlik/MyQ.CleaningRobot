using MyQ.CleaningRobot.Entities;

namespace MyQ.CleaningRobot.Helpers;

/// <summary>
/// Helper class for <see cref="CardinalDirection"/>.
/// </summary>
public static class CardinalDirectionHelper
{
    /// <summary>
    /// Maps a string representation of a cardinal direction to the corresponding <see cref="CardinalDirection"/> enum value.
    /// </summary>
    /// <param name="cardinalDirection">The string representation of the cardinal direction.</param>
    /// <returns>The corresponding <see cref="CardinalDirection"/> enum value.</returns>
    public static CardinalDirection MapCardinalDirection(string cardinalDirection)
    {
        return cardinalDirection.ToUpperInvariant() switch
        {
            "N" => CardinalDirection.North,
            "E" => CardinalDirection.East,
            "S" => CardinalDirection.South,
            "W" => CardinalDirection.West,
            _ => throw new ArgumentException($"Unknown {nameof(cardinalDirection)}: {cardinalDirection}"),
        };
    }

    /// <summary>
    /// Maps a <see cref="CardinalDirection"/> enum value to its string representation.
    /// </summary>
    /// <param name="cardinalDirection">The <see cref="CardinalDirection"/> enum value.</param>
    /// <returns>The string representation of the cardinal direction.</returns>
    public static string MapCardinalDirection(CardinalDirection cardinalDirection)
    {
        return cardinalDirection switch
        {
            CardinalDirection.North => "N",
            CardinalDirection.East => "E",
            CardinalDirection.South => "S",
            CardinalDirection.West => "W",
            _ => throw new ArgumentException($"Unknown {nameof(cardinalDirection)}: {cardinalDirection}"),
        };
    }
}
