using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Devices.Sensors;
using MauiApp1.Services;
using MauiApp1.Models;

namespace MauiApp1.ViewModels;

public class MapViewModel : INotifyPropertyChanged
{
    private readonly LocationService _locationService;
    private readonly GeofenceService _geofenceService;

    public MapViewModel(LocationService locationService, GeofenceService geofenceService)
    {
        _locationService = locationService;
        _geofenceService = geofenceService;
    }

    private Location? _currentLocation;
    public Location? CurrentLocation
    {
        get => _currentLocation;
        private set
        {
            _currentLocation = value;
            OnPropertyChanged();
        }
    }

    // POIs can be loaded from local file, API,... here as a simple list placeholder
    private List<Poi> _pois = new();
    public IReadOnlyList<Poi> Pois => _pois;

    public async Task UpdateLocationAsync()
    {
        var loc = await _locationService.GetCurrentLocationAsync();
        if (loc == null) return;

        CurrentLocation = loc;
        await _geofenceService.CheckLocationAsync(loc);
    }


    public void SetPois(IEnumerable<Poi> pois)
    {
        _pois = pois.ToList();
        _geofenceService.UpdatePois(_pois);
        OnPropertyChanged(nameof(Pois));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string name = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}