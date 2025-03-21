namespace IPFSTest.Interface;

public interface IIpfsService
{
    Task<string?> UploadFileAsync<T>(List<T> obj, string fileName = "data.json");
    public Task DownloadFileAsync(string cId, string outputPath);
}