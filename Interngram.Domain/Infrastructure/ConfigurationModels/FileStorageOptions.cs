namespace Interngram.Api.Infrastructure.ConfigurationModels;

public class FileStorageOptions
{
    public string StorageConnectionString { get; set; } = null!;
    public string ImageContainerName { get; set; } = null!;
}