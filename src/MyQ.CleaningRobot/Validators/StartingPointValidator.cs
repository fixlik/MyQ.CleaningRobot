﻿using MyQ.CleaningRobot.Business;
using MyQ.CleaningRobot.Entities.DTOs;

namespace MyQ.CleaningRobot.Validators;

/// <summary>
/// Validator for the starting point of the cleaning robot.
/// </summary>
public class StartingPointValidator(ICoordinateProvider coordinateProvider) : IStartingPointValidator
{
    /// <summary>
    /// Validates the starting point coordinates on the map.
    /// </summary>
    /// <param name="map">The map.</param>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    /// <returns><c>true</c> if the starting point is valid; otherwise, <c>false</c>.</returns>
    public bool Validate(MapDto map, int x, int y)
    {
        var cellType = coordinateProvider.GetCellType(map, x, y);

        if (cellType == null || cellType == Entities.CellType.CantBeOccupiedOrCleaned)
        {
            return false;
        }

        return true;
    }
}
