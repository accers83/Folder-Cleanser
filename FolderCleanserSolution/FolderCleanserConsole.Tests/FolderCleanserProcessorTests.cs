using FolderCleanserConsole.Processors;
using FolderCleanserFrontEndLibrary.DataAccess;
using FolderCleanserFrontEndLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace FolderCleanserConsole.Tests;

public class FolderCleanserProcessorTests
{
    [Theory]
    [InlineData(false, new string[0])]
    [InlineData(false, new string[] {"G:\\Temp\\Dummy"})]
    [InlineData(false, new string[] {"G:\\Temp\\Dummy", "H:\\Temp\\Test" })]
    [InlineData(true, new string[0])]
    public void When_No_Files_Returned_For_Path_Or_Deletes_Disabled_In_ProcessPath_No_Deletes_Occur(bool enableFileDeletes, string[] allFiles)
    {
        // Arrange
        var mockILogger = new Mock<ILogger<FolderCleanserProcessor>>();
        var iLogger = mockILogger.Object;

        var mockSectionEnableFileDeletes = new Mock<IConfigurationSection>();
        mockSectionEnableFileDeletes.Setup(x => x.Value).Returns(enableFileDeletes.ToString());
        var sectionEnableFileDeletes = mockSectionEnableFileDeletes.Object;

        var mockIConfig = new Mock<IConfiguration>();
        mockIConfig.Setup(x => x.GetSection("EnableFileDeletes")).Returns(sectionEnableFileDeletes);
        var iConfig = mockIConfig.Object;


        var mockIFileSystemRepository = new Mock<IFileSystemRepository>();

        mockIFileSystemRepository.Setup(x => x.GetAllFiles(It.IsAny<string>()))
                                 .Returns(allFiles.ToList())
                                 .Verifiable();

        mockIFileSystemRepository.Setup(x => x.DeleteFile(It.IsAny<string>()))
                                 .Verifiable();

        var iFileSystemRepository = mockIFileSystemRepository.Object;


        // Act
        var folderCleanserProcessor = new FolderCleanserProcessor(iFileSystemRepository, iLogger, iConfig);
        folderCleanserProcessor.ProcessPath(new PathModel());

        // Assert
        mockIFileSystemRepository.Verify(x => x.GetAllFiles(It.IsAny<string>()), Times.Once);
        mockIFileSystemRepository.Verify(x => x.DeleteFile(It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [InlineData(true, new string[] { "G:\\Temp\\Dummy" })]
    [InlineData(true, new string[] { "G:\\Temp\\Dummy", "H:\\Temp\\Test" })]
    public void When_Files_Returned_For_Path_And_Deletes_Enabled_In_ProcessPath_Deletes_Occur(bool enableFileDeletes, string[] allFiles)
    {
        // Arrange
        var mockILogger = new Mock<ILogger<FolderCleanserProcessor>>();
        var iLogger = mockILogger.Object;

        var mockSectionEnableFileDeletes = new Mock<IConfigurationSection>();
        mockSectionEnableFileDeletes.Setup(x => x.Value).Returns(enableFileDeletes.ToString());
        var sectionEnableFileDeletes = mockSectionEnableFileDeletes.Object;

        var mockIConfig = new Mock<IConfiguration>();
        mockIConfig.Setup(x => x.GetSection("EnableFileDeletes")).Returns(sectionEnableFileDeletes);
        var iConfig = mockIConfig.Object;


        var mockIFileSystemRepository = new Mock<IFileSystemRepository>();

        mockIFileSystemRepository.Setup(x => x.GetAllFiles(It.IsAny<string>()))
                                 .Returns(allFiles.ToList())
                                 .Verifiable();

        mockIFileSystemRepository.Setup(x => x.DeleteFile(It.IsAny<string>()))
                                 .Verifiable();

        var iFileSystemRepository = mockIFileSystemRepository.Object;


        // Act
        var folderCleanserProcessor = new FolderCleanserProcessor(iFileSystemRepository, iLogger, iConfig);
        folderCleanserProcessor.ProcessPath(new PathModel());

        // Assert
        mockIFileSystemRepository.Verify(x => x.GetAllFiles(It.IsAny<string>()), Times.Once);
        mockIFileSystemRepository.Verify(x => x.DeleteFile(It.IsAny<string>()), Times.AtLeastOnce);
    }
}
