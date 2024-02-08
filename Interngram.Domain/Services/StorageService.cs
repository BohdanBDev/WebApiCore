using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Interngram.Api.Infrastructure.ConfigurationModels;
using Interngram.Domain.Exceptions;
using Interngram.Domain.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Interngram.Domain.Services;

public class StorageService : IStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IOptions<FileStorageOptions> _fileStorageOptions;

    public StorageService(BlobServiceClient blobServiceClient, IOptions<FileStorageOptions> fileStorageOptions)
    {
        _blobServiceClient = blobServiceClient;
        _fileStorageOptions = fileStorageOptions;
    }
    
    public async Task UploadImageInBase64Async(string content, string fileName)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_fileStorageOptions.Value.ImageContainerName);
        await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
        var blobClient = blobContainerClient.GetBlobClient(fileName);
        var bytes = Convert.FromBase64String(content);
        using var stream = new MemoryStream(bytes); 
        await blobClient.UploadAsync(stream, new BlobHttpHeaders{ ContentType = "image/jpg"});
    }

    public async Task<string> GetImageInBase64Async(string fileName)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_fileStorageOptions.Value.ImageContainerName);
        if (!await blobContainerClient.ExistsAsync())
            throw new StorageException($"{_fileStorageOptions.Value.ImageContainerName} container doesn't exist");
        
        var blobClient = blobContainerClient.GetBlobClient(fileName);
        if (!await blobClient.ExistsAsync()) throw new StorageException($"{fileName} doesn't exist"); 
        
        BlobDownloadResult content = await blobClient.DownloadContentAsync();
        var convertedData = Convert.ToBase64String(content.Content.ToArray());
        
        return convertedData;
    }

    public async Task DeleteImageAsync(string fileName)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_fileStorageOptions.Value.ImageContainerName);
        if (!await blobContainerClient.ExistsAsync())
            throw new StorageException($"{_fileStorageOptions.Value.ImageContainerName} container doesn't exist");
        
        var blobClient = blobContainerClient.GetBlobClient(fileName);
        if (!await blobClient.ExistsAsync()) throw new StorageException($"{fileName} doesn't exist");

        await blobClient.DeleteAsync();
    }
}