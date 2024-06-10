using MyQ.CleaningRobot.Entities;

namespace MyQ.CleaningRobot.Helpers;

/// <summary>
/// Helper class for <see cref="CellType"/>.
/// </summary>
public static class CellTypeHelper
{
    /// <summary>
    /// Maps a cell type string to the corresponding <see cref="CellType"/> enum value.
    /// </summary>
    /// <param name="cellTypeString">The cell type string to map.</param>
    /// <returns>The mapped <see cref="CellType"/> enum value.</returns>
    public static CellType MapCellType(string cellTypeString)
    {
        return cellTypeString.ToUpperInvariant() switch
        {
            "S" => CellType.CleanableSpace,
            "C" => CellType.CantBeOccupiedOrCleaned,
            "NULL" or null or "" => CellType.Empty,
            _ => throw new ArgumentException($"Unknown cell type {cellTypeString}"),
        };
    }
}
