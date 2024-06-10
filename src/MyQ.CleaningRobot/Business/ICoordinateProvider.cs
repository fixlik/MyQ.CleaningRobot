using MyQ.CleaningRobot.Entities;
using MyQ.CleaningRobot.Entities.DTOs;

namespace MyQ.CleaningRobot.Business;

/// <summary>
/// Represents the interface for providing functionality for moving the cleaning robot on the map.
/// </summary>
public interface ICoordinateProvider
{
    /// <summary>
    /// Moves the cleaning robot on the map based on the given command.
    /// </summary>
    /// <param name="map">The map.</param>
    /// <param name="currentPosition">The current position of the cleaning robot.</param>
    /// <param name="command">The command.</param>
    /// <returns>The new position of the cleaning robot after the move.</returns>
    Position Move(MapDto map, Position currentPosition, CommandDto command);

    /// <summary>
    /// Gets the cell type at the specified coordinates on the map.
    /// </summary>
    /// <param name="map">The map.</param>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    /// <returns>The cell type at the specified coordinates.</returns>
    CellType? GetCellType(MapDto map, int x, int y);
}
