using MyQ.CleaningRobot.Business;
using MyQ.CleaningRobot.Entities;

namespace MyQ.CleaningRobot.UnitTests.Business;

public class CardinalDirectionRotatorTests
{
    [Theory]
    [InlineData(CardinalDirection.North, 360, CardinalDirection.North)]
    [InlineData(CardinalDirection.North, -360, CardinalDirection.North)]
    [InlineData(CardinalDirection.North, 90, CardinalDirection.East)]
    [InlineData(CardinalDirection.North, -90, CardinalDirection.West)]
    [InlineData(CardinalDirection.East, 180, CardinalDirection.West)]
    [InlineData(CardinalDirection.West, 1530, CardinalDirection.North)]
    [InlineData(CardinalDirection.West, -180, CardinalDirection.East)]
    public void Rotate_ShouldReturnExpected(CardinalDirection startDirection, int degrees, CardinalDirection expectedFinalDirection)
    {
        var cardinalDirectionManager = new CardinalDirectionRotator();

        var finalCardinalDirection = cardinalDirectionManager.Rotate(startDirection, degrees);

        Assert.Equal(expectedFinalDirection, finalCardinalDirection);
    }
}
