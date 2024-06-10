namespace MyQ.CleaningRobot.Entities;

/// <summary>
/// Represents a strategy for the cleaning robot.
/// </summary>
public record Strategy
{
    /// <summary>
    /// Type of the strategy.
    /// </summary>
    public StrategyType StrategyType { get; set; }

    /// <summary>
    /// Command sets for the strategy.
    /// </summary>
    public IEnumerable<IEnumerable<string>> CommandSets { get; set; }
}
