namespace MyQ.CleaningRobot.Entities;

/// <summary>
/// Represents the type of a cell in a cleaning robot's environment.
/// </summary>
public enum CellType
{
    CleanableSpace,
    CantBeOccupiedOrCleaned,
    Empty
}
