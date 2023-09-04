using Newtonsoft.Json;

namespace PSHEventManager;

public static class Extentions
{
    public static void Dump<T>(this T x)
    {
        string json = JsonConvert.SerializeObject(x, Formatting.Indented);
        Console.WriteLine(json);
    }
}