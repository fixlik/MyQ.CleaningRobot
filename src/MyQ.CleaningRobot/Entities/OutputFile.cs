﻿namespace MyQ.CleaningRobot.Entities;

/// <summary>
/// Represents the output file generated by the cleaning robot.
/// </summary>
public record OutputFile
{
    /// <summary>
    /// Positions visited by the cleaning robot.
    /// </summary>
    public IEnumerable<PositionBase> Visited { get; set; }

    /// <summary>
    /// Positions cleaned by the cleaning robot.
    /// </summary>
    public IEnumerable<PositionBase> Cleaned { get; set; }

    /// <summary>
    /// Final position of the cleaning robot.
    /// </summary>
    public InputOutputPosition Final { get; set; }

    /// <summary>
    /// Remaining battery level of the cleaning robot.
    /// </summary>
    public int Battery { get; set; }
}