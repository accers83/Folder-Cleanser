namespace FolderCleanserFrontEndLibrary.Helpers;

public class ConversionHelper : IConversionHelper
{
    public double ConvertBytesToMB(long bytes)
    {
        if (bytes < 0)
        {
            throw new ArgumentOutOfRangeException("bytes", "Parameter must >= 0");
        }
        
        double output = 0;

        output = ((double)bytes / 1024) / 1024;

        return output;
    }
}
