using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace GameProject.Extensions.Cache;

public class Cache<T> : ICache<T>
{
    private readonly string Path = System.IO.Path.GetFullPath(@"cache.json");
    private T? User;

    public async Task Clear()
    {
        await WriteFile(string.Empty);
        User = default;
    }

    public async Task<T> Set(T obj)
    {
        User = obj;
        await CreateFile();
        var json = JsonSerializer.Serialize(User);
        await WriteFile(json);

        return obj;
    }

    public async Task<string> ReadFile()
    {
        var res = await File.ReadAllTextAsync(Path);
        return res;
    }

    private async Task CreateFile()
    {
        if (!File.Exists(Path))
        {
            File.Create(Path);
        }
    }

    public async Task<T?> Get()
    {
        await CreateFile();
        var text = await ReadFile();
        if (User is null)
        {
            try
            {
                var user = JsonSerializer.Deserialize<T>(text);
                User = user;
                return user;
            }
            catch
            {
                return User;
            }
        }
        else
            return User;
    }

    public async Task WriteFile(string text)
    {
        try
        {
            await File.WriteAllTextAsync(Path, text);
        }
        catch { }
    }
}