using System.Text;
using System.Text.Json;
using IPFSTest.Interface;

namespace IPFSTest.Services;

public class IpfsService : IIpfsService
{
    private readonly HttpClient _httpClient;

    public IpfsService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(Const.Uri)
        };
    }
    
    public async Task<string?> UploadFileAsync<T>(List<T> obj, string fileName = "data.json")
    {
        try
        {
            string json = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(json));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(Const.ApplicationJson);

            using var form = new MultipartFormDataContent { { content,Const.File , fileName } }!;

            HttpResponseMessage response = await _httpClient.PostAsync(Const.Add, form);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Ошибка при загрузке в IPFS: {response.StatusCode}");
                return null;
            }

            string responseString = await response.Content.ReadAsStringAsync();
            using JsonDocument doc = JsonDocument.Parse(responseString);

            return doc.RootElement.GetProperty(Const.Hash).GetString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке в IPFS: {ex.Message}");
            return null;
        }
    }

    public async Task DownloadFileAsync(string cid, string outputPath)
    {
        if (cid.StartsWith("\"")) cid = cid[1..];
        if (cid.EndsWith("\"")) cid = cid[..^1];

        var gateways = new[]
        {
            Const.Gateway1,
            Const.Gateway2, 
            Const.Gateway3
        };

        foreach (var gateway in gateways)
        {
            var url = $"{gateway}{cid}";

            for (int i = 0; i < 3; i++)
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var fileContent = await response.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync(outputPath, fileContent);
                    Console.WriteLine("Файл скачан успешно!");
                    return;
                }
                Console.WriteLine($"Попытка {i + 1}: {response.StatusCode}");
                await Task.Delay(2000); 
            }
        }

        Console.WriteLine("Ошибка: файл не найден на всех шлюзах.");
    }
}

file record Const
{
    public const string Uri = "http://127.0.0.1:5003/api/v0/" ;
    public const string Add = "add" ;
    public const string ApplicationJson = "application/json" ;
    public const string File = "file";
    public const string Hash = "Hash";
    public const string Gateway1 = "http://127.0.0.1:8082/ipfs/";
    public const string Gateway2 = "https://ipfs.io/ipfs/";
    public const string Gateway3 = "https://dweb.link/ipfs/";


}