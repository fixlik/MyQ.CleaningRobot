using Microsoft.Extensions.Logging;
using MyQ.CleaningRobot.Entities;
using MyQ.CleaningRobot.Entities.DTOs;
using MyQ.CleaningRobot.Helpers;
using MyQ.CleaningRobot.Validators;

namespace MyQ.CleaningRobot.Business;

/// <summary>
/// Represents a cleaning robot that can clean and navigate a map.
/// </summary>
/// <param name="coordinateProvider">CoordinateProvider</param>
/// <param name="strategyProvider">StrategyProvider</param>
/// <param name="logger">Logger</param>
/// <param name="startingPointValidator">StartingPointValidator</param>
public class CleaningRobot(
        ICoordinateProvider coordinateProvider,
        IStrategyProvider strategyProvider,
        ILogger<CleaningRobot> logger,
        IStartingPointValidator startingPointValidator
    ) : ICleaningRobot
{
    private readonly List<PositionBase> cleanedCells = new();
    private readonly List<PositionBase> visitedCells = new();

    /// <summary>
    /// Gets the cells that have been cleaned by the robot.
    /// </summary>
    public IEnumerable<PositionBase> CleanedCells => cleanedCells.AsReadOnly();

    /// <summary>
    /// Gets the cells that have been visited by the robot.
    /// </summary>
    public IEnumerable<PositionBase> VisitedCells => visitedCells.AsReadOnly();

    /// <summary>
    /// Gets the remaining battery level of the robot.
    /// </summary>
    public int BatteryLeft { get; private set; }

    /// <summary>
    /// Gets or sets the current position of the robot.
    /// </summary>
    public Position CurrentPosition { get; private set; }

    /// <summary>
    /// Cleans the map based on the given input file.
    /// </summary>
    /// <param name="inputFile">The input file containing the map and commands.</param>
    /// <returns>The output file containing the visited cells, cleaned cells, final position, and battery level.</returns>
    public OutputFile Clean(InputFileDto inputFile)
    {
        Validate(inputFile);

        Init(new Position
        {
            X = inputFile.Start.X,
            Y = inputFile.Start.Y,
            Facing = inputFile.Start.Facing
        }, inputFile.Battery);

        foreach (var command in inputFile.Commands)
        {
            if (HasEnoughBattery(command.BatteryConsuption))
            {
                logger.LogInformation($"Processing command {command.CommandType}.");
                var canContinue = ProcessCommand(inputFile.Map, command);

                if (!canContinue)
                {
                    break;
                }
            }
            else
            {
                logger.LogWarning($"Not enough battery to continue cleaning. Command {command.CommandType} can't be performed.");
                break;
            }
        }

        return new OutputFile
        {
            Visited = VisitedCells.Select(pos => new PositionBase { X = pos.X, Y = pos.Y }).Distinct(),
            Cleaned = CleanedCells.Select(pos => new PositionBase { X = pos.X, Y = pos.Y }).Distinct(),
            Final = new InputOutputPosition
            {
                X = CurrentPosition.X,
                Y = CurrentPosition.Y,
                Facing = CardinalDirectionHelper.MapCardinalDirection(CurrentPosition.Facing)
            },
            Battery = BatteryLeft
        };
    }

    private void Validate(InputFileDto inputFile)
    {
        if (!startingPointValidator.Validate(inputFile.Map, inputFile.Start.X, inputFile.Start.Y))
        {
            throw new ArgumentException($"Starting point {inputFile.Start.X}, {inputFile.Start.Y} is outside of map or can't be occupied.");
        }
    }

    private void Init(Position currentPosition, int batteryLeft)
    {
        visitedCells.Clear();
        cleanedCells.Clear();

        CurrentPosition = currentPosition;
        visitedCells.Add(CurrentPosition);

        BatteryLeft = batteryLeft;
    }

    private bool ProcessCommand(MapDto map, CommandDto command)
    {
        return command.CommandType switch
        {
            CommandType.TurnLeft or CommandType.TurnRight or CommandType.Advance or CommandType.Back =>
                Move(map, command).CanContinue,
            CommandType.Clean =>
                Clean(command),
            _ =>
                throw new ArgumentException($"Unsupported {nameof(CommandType)}: {command.CommandType}")
        };
    }

    private bool Clean(CommandDto command)
    {
        DrainBattery(command.BatteryConsuption);

        cleanedCells.Add(CurrentPosition);

        return true;
    }

    private MoveResult Move(MapDto map, CommandDto command, bool executeBackOffStrategy = true)
    {
        DrainBattery(command.BatteryConsuption);

        var newPosition = coordinateProvider.Move(map, CurrentPosition, command);

        if (executeBackOffStrategy && newPosition == CurrentPosition)
        {
            logger.LogInformation($"Robot is stuck at position {CurrentPosition.X}, {CurrentPosition.Y}. Trying to execute back off strategy.");
            var backOffStrategy = strategyProvider.GetStrategies().Single(x => x.StrategyType == StrategyType.BackOffStrategy);

            logger.LogInformation($"Executing back off strategy {backOffStrategy.StrategyType}.");
            var unstuck = false;

            foreach (var commandSet in backOffStrategy.CommandSets)
            {
                var commandSetAsDtos = commandSet.Select(CommandHelper.CreateCommandDto);
                logger.LogInformation($"Executing back off command set [{string.Join(", ", commandSetAsDtos.Select(x => x.CommandType))}].");

                unstuck = ExecuteBackOffSet(map, commandSetAsDtos);

                if (unstuck)
                {
                    logger.LogInformation($"Back off command set successful.");
                    break;
                }

                logger.LogInformation($"Back off command set failed. Trying next command set.");
            }

            if (!unstuck)
            {
                logger.LogWarning($"Back off strategy failed. Robot is stuck at position {CurrentPosition.X}, {CurrentPosition.Y}.");
                return new MoveResult
                {
                    CanContinue = false,
                    NewPosition = newPosition
                };
            }
        }
        else
        {
            visitedCells.Add(newPosition);
            CurrentPosition = newPosition;
        }

        return new MoveResult
        {
            CanContinue = true,
            NewPosition = newPosition
        };
    }

    private bool ExecuteBackOffSet(MapDto map, IEnumerable<CommandDto> commandSet)
    {
        Position previousPosition = null;
        foreach (var backOffCommand in commandSet)
        {
            if (HasEnoughBattery(backOffCommand.BatteryConsuption))
            {
                var moveResult = Move(map, backOffCommand, false);

                if (previousPosition == null)
                {
                    previousPosition = moveResult.NewPosition;
                }
                else if (previousPosition == moveResult.NewPosition)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void DrainBattery(int batteryConsumption) => BatteryLeft -= batteryConsumption;

    private bool HasEnoughBattery(int batteryConsumption) => BatteryLeft >= batteryConsumption;
}
