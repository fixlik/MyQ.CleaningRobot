using MyQ.CleaningRobot.Entities.DTOs;

namespace MyQ.CleaningRobot.Helpers;

/// <summary>
/// Helper class for operations with Map.
/// </summary>
public static class MapHelper
{
    /// <summary>
    /// Creates a MapDto object from a given map.
    /// </summary>
    /// <param name="map">The map represented as a collection of rows, where each row is a collection of strings.</param>
    /// <returns>The MapDto object representing the map.</returns>
    public static MapDto CreateMapDto(this IEnumerable<IEnumerable<string>> map)
    {
        var mapItems = map.Select(row => row.Select(column => CellTypeHelper.MapCellType(column)));

        return new MapDto
        {
            Matrix = mapItems
        };
    }
}
