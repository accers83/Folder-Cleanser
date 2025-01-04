using FolderCleanserFrontEndLibrary.Helpers;

namespace FolderCleanserFrontEndLibrary.Tests;

public class ConversionHelperTests
{
    [Fact]
    public void ConvertBytesToMb_Should_Return_Zero()
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
}
