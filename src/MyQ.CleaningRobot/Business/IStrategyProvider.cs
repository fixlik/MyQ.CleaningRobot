using MyQ.CleaningRobot.Entities;

namespace MyQ.CleaningRobot.Business;

public interface IStrategyProvider
{
    IEnumerable<Strategy> GetStrategies();
}
