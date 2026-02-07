using Microsoft.Maui.Devices.Sensors;

namespace MauiApp1.Services;

public class LocationService
{
    public async Task<Location?> GetCurrentLocationAsync()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
                return null;
        }

        var request = new GeolocationRequest(
            GeolocationAccuracy.Best,
            TimeSpan.FromSeconds(10)
        );

        return await Geolocation.GetLocationAsync(request);
    }
}
