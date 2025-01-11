using System.Text.Json;
using MyApp.Model;

namespace MyApp.Repositories;

public class Repository<T>
{
    protected static List<T>? Items = new List<T>();
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions();
    
    public Repository(string filePath)
    {
        try
        {
            _options.Converters.Add(new DateTimeConverterUsingDateTimeParse());
            using var sr = new StreamReader(filePath);
            var jsonTemp = sr.ReadToEnd();
            Items = JsonSerializer.Deserialize<List<T>>(jsonTemp, _options);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"[Error] File not found: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"[Error] Error deserializing JSON: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] An error occurred: {ex.Message}");
        }
    }
}