using Microsoft.Extensions.Logging.Abstractions;
using MyQ.CleaningRobot.Business;
using MyQ.CleaningRobot.Entities.DTOs;
using MyQ.CleaningRobot.Entities;
using MyQ.CleaningRobot.Validators;

namespace MyQ.CleaningRobot.UnitTests.Business;

public class CleaningRobotTests
{
    [Fact]
    public void Clean_ShouldThrowException()
    {
        var cleaningRobot = GetCleaningRobot();

        var inputFile = new InputFileDto
        {
            Start = new InputOutputPositionDto { X = 0, Y = 0, Facing = CardinalDirection.North },
            Battery = 100,
            Map = new MapDto
            {
                Matrix =
                [
                    [ CellType.CantBeOccupiedOrCleaned ]
                ]
            },
            Commands =
            [
               new CommandDto { CommandType = CommandType.Advance, BatteryConsuption = 1, DegreesOfRotation = 0 }
            ]
        };

        Assert.Throws<ArgumentException>(() => cleaningRobot.Clean(inputFile));

        //var outputFile = cleaningRobot.Clean(inputFile);

        //Assert.NotNull(outputFile);
        //Assert.Single(outputFile.Visited);
        //Assert.Empty(outputFile.Cleaned);
        //Assert.Equal(0, outputFile.Final.X);
        //Assert.Equal(0, outputFile.Final.Y);
    }

    [Fact]
    public void Clean_ShouldInitiateBackOffSequence()
    {
        var cleaningRobot = GetCleaningRobot();

        var inputFile = new InputFileDto
        {
            Start = new InputOutputPositionDto { X = 2, Y = 0, Facing = CardinalDirection.North },
            Battery = 100,
            Map = new MapDto
            {
                Matrix =
                [
                    [ CellType.CleanableSpace, CellType.CleanableSpace, CellType.CleanableSpace ],
                    [ CellType.CleanableSpace, CellType.CleanableSpace, CellType.CleanableSpace ]
                ]
            },
            Commands =
            [
               new CommandDto { CommandType = CommandType.Advance, BatteryConsuption = 1, DegreesOfRotation = 0 }
            ]
        };

        var outputFile = cleaningRobot.Clean(inputFile);

        Assert.NotNull(outputFile);
        Assert.Equal(2, outputFile.Visited.Count());
        Assert.Empty(outputFile.Cleaned);
        Assert.Equal(2, outputFile.Final.X);
        Assert.Equal(1, outputFile.Final.Y);
    }

    [Fact]
    public void Clean_NotEnoughBattery()
    {
        var cleaningRobot = GetCleaningRobot();

        var inputFile = new InputFileDto
        {
            Start = new InputOutputPositionDto { X = 1, Y = 1, Facing = CardinalDirection.North },
            Battery = 5,
            Map = new MapDto
            {
                Matrix =
                [
                    [ CellType.CleanableSpace, CellType.CleanableSpace, CellType.CleanableSpace ],
                    [ CellType.CleanableSpace, CellType.CleanableSpace, CellType.CleanableSpace ]
                ]
            },
            Commands =
            [
               new CommandDto { CommandType = CommandType.Advance, BatteryConsuption = 10, DegreesOfRotation = 0 }
            ]
        };

        var outputFile = cleaningRobot.Clean(inputFile);

        Assert.NotNull(outputFile);
        Assert.Single(outputFile.Visited);
        Assert.Empty(outputFile.Cleaned);
        Assert.Equal(1, outputFile.Final.X);
        Assert.Equal(1, outputFile.Final.Y);
    }

    private CleaningRobot.Business.CleaningRobot GetCleaningRobot()
    {
        var cardinalDirectionRotator = new CardinalDirectionRotator();
        var coordinateProvider = new CoordinateProvider(cardinalDirectionRotator);
        var strategyProvider = new StrategyProvider();
        var logger = new NullLogger<CleaningRobot.Business.CleaningRobot>();
        var startingPointValidator = new StartingPointValidator(coordinateProvider);
        var cleaningRobot = new CleaningRobot.Business.CleaningRobot(coordinateProvider, strategyProvider, logger, startingPointValidator);

        return cleaningRobot;
    }
}
