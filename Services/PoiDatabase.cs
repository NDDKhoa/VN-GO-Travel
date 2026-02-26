using SQLite;
using MauiApp1.Models;

namespace MauiApp1.Services;

public class PoiDatabase
{
    private readonly SQLiteAsyncConnection _db;
    private bool _inited;

    public PoiDatabase()
    {
        var path = Path.Combine(
            FileSystem.AppDataDirectory,
            "pois.db"
        );

        _db = new SQLiteAsyncConnection(path);
    }

    public async Task InitAsync()
    {
        if (_inited) return;

        await _db.CreateTableAsync<Poi>();

        _inited = true;
    }

    public Task<List<Poi>> GetAllAsync()
        => _db.Table<Poi>().ToListAsync();

    public Task<int> InsertAsync(Poi poi)
        => _db.InsertAsync(poi);

    public Task<int> InsertManyAsync(IEnumerable<Poi> pois)
        => _db.InsertAllAsync(pois);
}