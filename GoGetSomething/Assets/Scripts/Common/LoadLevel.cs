/**
 * LoadLevel.cs
 * Created by Akeru on 13/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

static class LoadLevel
{
    public static string SceneName;
    public static SongsEnum SceneSongId;

    public static void Load(string sceneName, bool special = false)
    {
        SceneName = sceneName;
        SceneSongId = MusicController.I != null ? MusicController.I.CurrentSong : SongsEnum.Main;
        //TODO canviar musica lol
        if (LoadingController.I != null) LoadingController.I.LoadScene(special);
    }

    public static void Load(string sceneName, SongsEnum songId, bool special = false)
    {
        SceneName = sceneName;
        SceneSongId = songId;
        //TODO canviar musica lol
        if (LoadingController.I != null) LoadingController.I.LoadScene(special);
    }
}