namespace MauiApp1.Models;

public class Poi
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    // Localized versions (language code -> string), optional
    public Dictionary<string, string> LocalizedNames { get; set; } = new();
    public Dictionary<string, string> LocalizedDescriptions { get; set; } = new();

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public double Radius { get; set; } = 50; // mét
    public int Priority { get; set; } = 1;

    // Helper để lấy văn bản theo ngôn ngữ, fallback về Name/Description
    public string GetName(string languageCode = "en")
        => LocalizedNames.TryGetValue(languageCode, out var n) ? n : Name;

    public string GetDescription(string languageCode = "en")
        => LocalizedDescriptions.TryGetValue(languageCode, out var d) ? d : Description;
}