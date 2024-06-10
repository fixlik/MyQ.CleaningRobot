namespace MyQ.CleaningRobot.Entities.DTOs;

/// <summary>
/// Represents a data transfer object for a map.
/// </summary>
public record MapDto
{
    /// <summary>
    /// Matrix representing the map.
    /// </summary>
    public required IEnumerable<IEnumerable<CellType>> Matrix { get; set; }
}
