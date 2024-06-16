using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using GameProject.Models;

namespace GameProject.Extensions;


public static class SerializeFile
{
    private static readonly string Path = Config.Config.SlidesFile;

    public static async Task<Slide[]> Serialize()
    {
        var text = await ReadFile();
        
        return JsonSerializer.Deserialize<Slide[]>(text);
    }

    private static async Task<string> ReadFile()
    {
        var res = await File.ReadAllTextAsync(Path);
        return res;
    }
}