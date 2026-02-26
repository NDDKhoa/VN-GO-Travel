using Microsoft.Maui.Devices.Sensors;

namespace MauiApp1.Services;

public class LocationService
{
    private bool _permissionGranted;

    public async Task<Location?> GetCurrentLocationAsync()
    {
        if (!_permissionGranted)
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted) return null;
            _permissionGranted = true;
        }

        return await Geolocation.GetLocationAsync(
            new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10))
        );
    }
}
