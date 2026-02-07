using Microsoft.Maui.Media;

namespace MauiApp1.Services;

public class AudioService
{
    private bool _isSpeaking;
    public async Task SpeakAsync(string text)
    {
        if (_isSpeaking) return;

        _isSpeaking = true;
        await TextToSpeech.SpeakAsync(text);
        _isSpeaking = false;
    }

}
