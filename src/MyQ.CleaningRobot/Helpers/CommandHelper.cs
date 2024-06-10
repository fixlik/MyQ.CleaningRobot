using MyQ.CleaningRobot.Entities;
using MyQ.CleaningRobot.Entities.DTOs;

namespace MyQ.CleaningRobot.Helpers;

/// <summary>
/// A helper class for Commands.
/// </summary>
public static class CommandHelper
{
    /// <summary>
    /// Creates a command DTO based on the given command type string.
    /// </summary>
    /// <param name="commandTypeString">The command type string.</param>
    /// <returns>The created command DTO.</returns>
    public static CommandDto CreateCommandDto(string commandTypeString)
    {
        var commandType = MapCommandType(commandTypeString);
        var batteryConsumption = GetBatteryConsumption(commandType);
        var degreesOfRotation = GetDegreesOfRotation(commandType);

        return new CommandDto
        {
            CommandType = commandType,
            BatteryConsuption = batteryConsumption,
            DegreesOfRotation = degreesOfRotation
        };
    }

    /// <summary>
    /// Maps the command type string to the corresponding CommandType enum value.
    /// </summary>
    /// <param name="commandTypeString">The command type string.</param>
    /// <returns>The mapped CommandType enum value.</returns>
    public static CommandType MapCommandType(string commandTypeString)
    {
        return commandTypeString.ToUpperInvariant() switch
        {
            "TL" => CommandType.TurnLeft,
            "TR" => CommandType.TurnRight,
            "A" => CommandType.Advance,
            "B" => CommandType.Back,
            "C" => CommandType.Clean,
            _ => throw new ArgumentException($"Invalid {nameof(commandTypeString)}: {commandTypeString}.")
        };
    }

    /// <summary>
    /// Maps the CommandType enum value to the corresponding command type string.
    /// </summary>
    /// <param name="commandType">The CommandType enum value.</param>
    /// <returns>The mapped command type string.</returns>
    public static string MapCommandType(CommandType commandType)
    {
        return commandType switch
        {
            CommandType.TurnLeft => "TL",
            CommandType.TurnRight => "TR",
            CommandType.Advance => "A",
            CommandType.Back => "B",
            CommandType.Clean => "C",
            _ => throw new ArgumentException($"Invalid {nameof(commandType)}: {commandType}.")
        };
    }

    /// <summary>
    /// Gets the battery consumption value for the given command type.
    /// </summary>
    /// <param name="commandType">The command type.</param>
    /// <returns>The battery consumption value.</returns>
    private static int GetBatteryConsumption(CommandType commandType)
    {
        return commandType switch
        {
            CommandType.TurnLeft or CommandType.TurnRight => 1,
            CommandType.Advance => 2,
            CommandType.Back => 3,
            CommandType.Clean => 5,
            _ => throw new ArgumentException($"Unknown battery consumption for {nameof(commandType)}: {commandType}")
        };
    }

    /// <summary>
    /// Gets the degrees of rotation for the given command type.
    /// </summary>
    /// <param name="commandType">The command type.</param>
    /// <returns>The degrees of rotation.</returns>
    private static int GetDegreesOfRotation(CommandType commandType)
    {
        return commandType switch
        {
            CommandType.TurnLeft => -90,
            CommandType.TurnRight => 90,
            CommandType.Advance or CommandType.Back or CommandType.Clean => 0,
            _ => throw new ArgumentException($"Unknown degrees of rotation for {nameof(commandType)}: {commandType}")
        };
    }
}
