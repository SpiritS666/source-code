using System.Threading.Tasks;

namespace GameProject.Extensions.Cache;

public interface ICache<T>
{
    Task<T?> Get();
    Task<T> Set(T obj);
    Task Clear();

    Task<string> ReadFile();
    Task WriteFile(string text);
}