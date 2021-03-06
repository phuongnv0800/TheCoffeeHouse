using TCH.BackendApi.Repositories.DataRepository;

namespace TCH.BackendApi.Repositories.DataManager;
public class FileStorageService : IStorageService
{
    private readonly string _userContentFolder;
    private const string UserContentFolderName = "Uploads";

    public FileStorageService(IWebHostEnvironment webHostEnvironment)
    {
        _userContentFolder = Path.Combine(Directory.GetCurrentDirectory(), UserContentFolderName);
    }
    public string GetFileUrl(string fileName)
    {
        return $"/{UserContentFolderName}/{fileName}";
    }

    public string GetPathBE(string fileName)
    {
        return $"{_userContentFolder}/{fileName}";
    }
    public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
    {
        var filePath = Path.Combine(_userContentFolder, fileName);
        using var output = new FileStream(filePath, FileMode.Create);
        await mediaBinaryStream.CopyToAsync(output);
    }

    public async Task DeleteFileAsync(string fileName)
    {
        var filePath = Path.Combine(_userContentFolder, fileName);
        if (File.Exists(filePath))
        {
            await Task.Run(() => File.Delete(filePath));
        }
    }
}
