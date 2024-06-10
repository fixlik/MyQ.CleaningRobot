namespace MyQ.CleaningRobot.Entities;

/// <summary>
/// Represents the type of command for the cleaning robot.
/// </summary>
public enum CommandType
{
    TurnLeft,
    TurnRight,
    Advance,
    Back,
    Clean
}
