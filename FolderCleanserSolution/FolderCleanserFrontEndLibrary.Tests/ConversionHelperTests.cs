using FolderCleanserFrontEndLibrary.Helpers;

namespace FolderCleanserFrontEndLibrary.Tests;

public class ConversionHelperTests
{
    [Fact]
    public void When_Bytes_Zero_ConvertBytesToMb_Should_Return_Zero()
    {
        // Arrange
        var conversionHelper = new ConversionHelper();
        long bytes = 0;
        double expected = 0;

        // Act 
        double actual = conversionHelper.ConvertBytesToMB(bytes);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-1001)]
    [InlineData(long.MinValue)]
    public void When_Bytes_Negative_ConvertBytesToMb_Should_Throw_Exception(long bytes)
    {
        // Arrange
        var conversionHelper = new ConversionHelper();

        // Act 
        Action convertBytesToMb = () => conversionHelper.ConvertBytesToMB(bytes);

        // Assert
        Assert.ThrowsAny<ArgumentOutOfRangeException>(convertBytesToMb);
    }

    [Theory]
    [InlineData(1, 9.5367431640625E-07)]
    [InlineData(1000000, 0.95367431640625)]
    [InlineData(long.MaxValue, 8796093022208)]
    public void When_Bytes_Positive_ConvertBytesToMb_Should_Return_Value(long bytes, double expected)
    {
        // Arrange
        var conversionHelper = new ConversionHelper();

        // Act 
        double actual = conversionHelper.ConvertBytesToMB(bytes);

        // Assert
        Assert.Equal(expected, actual);
    }
}
