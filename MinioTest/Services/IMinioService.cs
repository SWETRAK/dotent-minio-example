namespace MinioTest.Services;

public interface IMinioService
{
    public Task InsertFile(string objectName, string contentType, MemoryStream fileStream);

    public Task<(MemoryStream, string)> GetFile(string objectName);

    public Task RemoveFile(string objectName);
}