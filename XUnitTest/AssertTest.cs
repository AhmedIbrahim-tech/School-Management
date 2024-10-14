using FluentAssertions;

namespace XUnitTest;

public class AssertTest
{
    [Fact]
    public void Calculate_2_Sum_3_shoud_be_5()
    {
        // Arrane
        int x = 2; int y = 3; int z;

        // Act
        z = x + y;

        // Assert
        Assert.Equal(5 , z);
    }




    [Fact]
    public void Calculate_2_Sum_3_shoud_be_5_with_fluentAssertions()
    {
        // Arrane
        int x = 2; int y = 3; int z;

        // Act
        z = x + y;

        // Assert
        z.Should().Be(5,"X Sum Y not equal 7");
    }


    [Fact]
    public void String_Should_be_starting_with_we()
    {
        var Grates = "Welcome";
        Grates.Should().StartWith("We");
    }
}