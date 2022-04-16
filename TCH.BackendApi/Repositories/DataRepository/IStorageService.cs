namespace TCH.BackendApi.Repositories.DataRepository;

public interface IStorageService
{
    string GetFileUrl(string fileName);

    Task SaveFileAsync(Stream mediaBinaryStream, string fileName);

    Task DeleteFileAsync(string fileName);
}
