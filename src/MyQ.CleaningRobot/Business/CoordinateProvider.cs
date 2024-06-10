using MyQ.CleaningRobot.Entities;
using MyQ.CleaningRobot.Entities.DTOs;

namespace MyQ.CleaningRobot.Business;

/// <summary>
/// Provides functionality for moving the cleaning robot on the map.
/// </summary>
public class CoordinateProvider(ICardinalDirectionRotator cardinalDirectionRotator) : ICoordinateProvider
{
    /// <summary>
    /// Moves the cleaning robot on the map based on the given command.
    /// </summary>
    /// <param name="map">The map.</param>
    /// <param name="currentPosition">The current position of the cleaning robot.</param>
    /// <param name="command">The command.</param>
    /// <returns>The new position of the cleaning robot after the move.</returns>
    public Position Move(MapDto map, Position currentPosition, CommandDto command)
    {
        return command.CommandType switch
        {
            CommandType.TurnLeft => Rotate(map, currentPosition, command.DegreesOfRotation),
            CommandType.TurnRight => Rotate(map, currentPosition, command.DegreesOfRotation),
            CommandType.Advance => Advance(map, currentPosition),
            CommandType.Back => Advance(map, currentPosition, true),
            CommandType.Clean => currentPosition,
            _ => throw new ArgumentException($"Unsupported {nameof(CommandType)}: {command.CommandType}")
        };
    }

    /// <summary>
    /// Gets the cell type at the specified coordinates on the map.
    /// </summary>
    /// <param name="map">The map.</param>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    /// <returns>The cell type at the specified coordinates.</returns>
    public CellType? GetCellType(MapDto map, int x, int y)
    {
        if (y < 0 || y > (map.Matrix.Count() - 1))
        {
            return null;
        }

        var yElement = map.Matrix.ElementAt(y);
        if (x < 0 || x > (yElement.Count() - 1))
        {
            return null;
        }
                
        return yElement?.ElementAt(x);
    }

    private Position Rotate(MapDto map, Position currentPosition, int degrees)
    {
        var x = currentPosition.X;
        var y = currentPosition.Y;

        var facing = cardinalDirectionRotator.Rotate(currentPosition.Facing, degrees);

        return new Position
        {
            X = x,
            Y = y,
            Facing = facing,
            CellType = GetCellType(map, x, y)
        };
    }

    private Position Advance(MapDto map, Position currentPosition, bool reverse = false)
    {
        var coefficient = reverse ? -1 : 1;

        return currentPosition.Facing switch
        {
            CardinalDirection.North => MoveVertical(map, currentPosition, -1 * coefficient),
            CardinalDirection.East => MoveHorizontal(map, currentPosition, 1 * coefficient),
            CardinalDirection.South => MoveVertical(map, currentPosition, 1 * coefficient),
            CardinalDirection.West => MoveHorizontal(map, currentPosition, -1 * coefficient),
            _ => throw new ArgumentException($"Unsupported {nameof(CardinalDirection)}: {currentPosition.Facing}")
        };
    }

    private Position MoveHorizontal(MapDto map, Position currentPosition, int steps)
    {
        var x = currentPosition.X + steps;
        var y = currentPosition.Y;

        var newCellType = GetCellType(map, x, y);

        if (newCellType == null || newCellType == CellType.CantBeOccupiedOrCleaned)
        {
            return currentPosition;
        }

        return new Position
        {
            X = x,
            Y = y,
            Facing = currentPosition.Facing,
            CellType = newCellType
        };
    }

    private Position MoveVertical(MapDto map, Position currentPosition, int steps)
    {
        var x = currentPosition.X;
        var y = currentPosition.Y + steps;

        var newCellType = GetCellType(map, x, y);

        if (newCellType == null)
        {
            return currentPosition;
        }

        return new Position
        {
            X = x,
            Y = y,
            Facing = currentPosition.Facing,
            CellType = newCellType
        };
    }
}
