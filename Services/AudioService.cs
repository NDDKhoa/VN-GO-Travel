using Microsoft.Maui.Media;

namespace MauiApp1.Services;

public class AudioService
{
    private readonly SemaphoreSlim _audioGate = new(1, 1);

    public async Task SpeakAsync(string text)
    {
        if (!await _audioGate.WaitAsync(0)) return;
        try
        {
            await TextToSpeech.SpeakAsync(text);
        }
        finally
        {
            _audioGate.Release();
        }
    }

}
