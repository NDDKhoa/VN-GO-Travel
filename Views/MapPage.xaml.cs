using MauiApp1.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace MauiApp1.Views;

public partial class MapPage : ContentPage
{
    private readonly MapViewModel _vm;
    private PeriodicTimer? _timer;
    private bool _isTracking;

    public MapPage(MapViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;

        LoadSamplePois(); // 🔥 BẮT BUỘC – nếu không map trống
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_isTracking) return;

        _isTracking = true;
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
        _ = StartTrackingAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isTracking = false;

        _timer?.Dispose();
        _timer = null;
    }


    private async Task StartTrackingAsync()
    {
        if (_timer == null) return;

        while (_timer != null && await _timer.WaitForNextTickAsync())
        {
            await _vm.UpdateLocationAsync();

            var location = _vm.CurrentLocation;
            if (location == null) continue;

            var center = new Location(
                location.Latitude,
                location.Longitude
            );

            Map.MoveToRegion(
                MapSpan.FromCenterAndRadius(center, Distance.FromMeters(200))
            );

            Map.Pins.Clear();
            Map.MapElements.Clear();

            foreach (var poi in _vm.Pois)
            {
                var pin = new Pin
                {
                    Label = poi.GetName("en"),
                    Address = poi.GetDescription("en"),
                    Location = new Location(poi.Latitude, poi.Longitude)
                };

                Map.Pins.Add(pin);

                Map.MapElements.Add(new Circle
                {
                    Center = pin.Location,
                    Radius = Distance.FromMeters(poi.Radius)
                });
            }
        }
    }

    private void LoadSamplePois()
    {
        _vm.SetPois(new[]
        {
            new Models.Poi
            {
                Id = 1,
                Name = "Dinh Độc Lập",
                Description = "Di tích lịch sử quốc gia đặc biệt",
                Latitude = 10.7769,
                Longitude = 106.6953,
                Radius = 100,
                Priority = 2
            },
            new Models.Poi
            {
                Id = 2,
                Name = "Nhà thờ Đức Bà",
                Description = "Biểu tượng kiến trúc Pháp",
                Latitude = 10.7798,
                Longitude = 106.6992,
                Radius = 80,
                Priority = 1
            }
        });
    }
}
