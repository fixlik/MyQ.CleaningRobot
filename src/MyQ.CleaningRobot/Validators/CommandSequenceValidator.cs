using MyQ.CleaningRobot.Entities.DTOs;

namespace MyQ.CleaningRobot.Validators;

/// <summary>
/// Validator for validating a sequence of commands.
/// </summary>
public class CommandSequenceValidator : ICommandSequenceValidator
{
    /// <summary>
    /// Validates the given sequence of commands based on the remaining battery level.
    /// </summary>
    /// <param name="commands">The sequence of commands to validate.</param>
    /// <param name="remainingBattery">The remaining battery level of the cleaning robot.</param>
    /// <returns><c>true</c> if the sequence of commands is valid based on the remaining battery level, otherwise <c>false</c>.</returns>
    public bool Validate(IEnumerable<CommandDto> commands, int remainingBattery)
    {
        var batteryDrainage = commands.Sum(x => x.BatteryConsuption);

        return batteryDrainage <= remainingBattery;
    }
}
