using Microsoft.Xna.Framework.Media;


namespace GameProject.Extensions.RecordPlayer;

public static class RecordPlayer
{
    public static void SetMusic(Song music)
    {
        MediaPlayer.Play(music);
        MediaPlayer.IsRepeating = true;
    }

    public static void SetMusicVolume(int volume)
    {
        volume = VolumeCheck(volume);
        MediaPlayer.Volume = (float)volume / 100;
    }

    private static int VolumeCheck(int volume)
    {
        if (volume < 0)
            return 0;
        else if (volume > 100)
            return 100;

        return volume;
    }
}