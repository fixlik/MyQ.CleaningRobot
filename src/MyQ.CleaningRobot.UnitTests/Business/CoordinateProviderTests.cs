using MyQ.CleaningRobot.Business;
using MyQ.CleaningRobot.Entities;
using MyQ.CleaningRobot.Entities.DTOs;
using MyQ.CleaningRobot.Helpers;
using System.Text.Json;

namespace MyQ.CleaningRobot.UnitTests.Business;

public class CoordinateProviderTests
{
    [Theory]
    [InlineData(2, 0, CardinalDirection.East, CellType.CleanableSpace, CommandType.Advance, 3, 0, CardinalDirection.East, CellType.CleanableSpace)]
    [InlineData(3, 0, CardinalDirection.North, CellType.CleanableSpace, CommandType.Advance, 3, 0, CardinalDirection.North, CellType.CleanableSpace)]
    public void Move_ShouldReturnExpected(
            int x,
            int y,
            CardinalDirection facing,
            CellType cellType,
            CommandType commandType,
            int expectedX,
            int expectedY,
            CardinalDirection expectedFacing,
            CellType expectedCellType
        )
    {
        var currentPosition = new Position
        {
            X = x,
            Y = y,
            Facing = facing,
            CellType = cellType
        };
        var expectedPosition = new Position
        {
            X = expectedX,
            Y = expectedY,
            Facing = expectedFacing,
            CellType = expectedCellType
        };

        var command = CommandHelper.CreateCommandDto(CommandHelper.MapCommandType(commandType));

        var cardinalDirectionRotator = new CardinalDirectionRotator();
        var map = GetMap();

        var coordinateProvider = new CoordinateProvider(cardinalDirectionRotator);

        var newPosition = coordinateProvider.Move(map, currentPosition, command);

        Assert.Equal(expectedPosition, newPosition);
    }

    private static MapDto GetMap()
    {
        var mapString = "[\r\n" +
            "[\"S\", \"S\", \"S\", \"S\"],\r\n" +
            "[\"S\", \"S\", \"C\", \"S\"],\r\n" +
            "[\"S\", \"S\", \"S\", \"S\"],\r\n" +
            "[\"S\", \"null\", \"S\", \"S\"]\r\n" +
            "]";

        var map = JsonSerializer.Deserialize<IEnumerable<IEnumerable<string>>>(mapString);
        var mapDto = MapHelper.CreateMapDto(map);

        return mapDto;
    }
}
