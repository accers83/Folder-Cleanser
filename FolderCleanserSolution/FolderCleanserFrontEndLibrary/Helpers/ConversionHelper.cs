namespace FolderCleanserFrontEndLibrary.Helpers;

public class ConversionHelper : IConversionHelper
{
    public double ConvertBytesToMB(long bytes)
    {
        double output = 0;

        output = ((double)bytes / 1024) / 1024;

        return output;
    }
}
