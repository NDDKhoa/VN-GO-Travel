using SQLite;

namespace MauiApp1.Models;

[Table("pois")]
public class Poi
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public double Radius { get; set; } = 50;
    public int Priority { get; set; } = 1;

    // ❌ Không lưu localization vào DB (để sau)
    [Ignore] public Dictionary<string, string> LocalizedNames { get; set; } = new();
    [Ignore] public Dictionary<string, string> LocalizedDescriptions { get; set; } = new();

    public string GetName(string lang = "en") => Name;
    public string GetDescription(string lang = "en") => Description;
}