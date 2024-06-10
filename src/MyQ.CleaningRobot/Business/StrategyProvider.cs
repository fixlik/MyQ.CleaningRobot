using MyQ.CleaningRobot.Entities;

namespace MyQ.CleaningRobot.Business;

public class StrategyProvider : IStrategyProvider
{
    public IEnumerable<Strategy> GetStrategies()
    {
        return new[] { new Strategy
            {
                StrategyType = StrategyType.BackOffStrategy,
                CommandSets = new[]
                {
                    new[] { "TR", "A", "TL" },
                    new[] { "TR", "A", "TR" },
                    new[] { "TR", "A", "TR" },
                    new[] { "TR", "B", "TR", "A" },
                    new[] { "TL", "TL", "A" }
                }
            }
        };
    }
}
