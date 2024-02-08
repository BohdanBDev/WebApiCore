namespace Interngram.Domain.Services.Interfaces;

public interface IStorageService
{
    public Task UploadImageInBase64Async(string content, string fileName);
    public Task<string> GetImageInBase64Async(string fileName);
    public Task DeleteImageAsync(string fileName);
}