using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonManager<AudioManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

    private const string MAIN = "MAIN_THEME";
    private const string TEMPLE = "TEMPLE_WORLD_THEME";
    private const string JUNGLE = "JUNGLE_WORLD_THEME";
    private const string SPACE = "SPACE_WORLD_THEME";

    public AudioSource mainTheme;
    public AudioSource templeTheme;
    public AudioSource jungleTheme;
    public AudioSource spaceTheme;

    private List<AudioSource> musics;

    protected override void Init() {
        musics = new List<AudioSource> {
            mainTheme,
            templeTheme,
            jungleTheme,
            spaceTheme
        };
        MainTheme();
    }

    private void StopAllMusic() {
        foreach (var m in musics) {
            m.Stop();
        }
    }

    private AudioClip GetClip(string source) {
        return Resources.Load("Audio/Music/" + source) as AudioClip;
    }

    private void PlayMusic(AudioSource source, AudioClip clip) {
        StopAllMusic();
        source.clip = clip;
        source.Play();
    }

    public void MainTheme() {
        PlayMusic(mainTheme, GetClip(MAIN));
    }

    public void TempleMusic() {
        if (templeTheme.isPlaying) return;
            PlayMusic(templeTheme, GetClip(TEMPLE));

    }

    public void JungleMusic() {
        if (jungleTheme.isPlaying) return;
        PlayMusic(jungleTheme, GetClip(JUNGLE));
    }

    public void SpaceMusic() {
        if (spaceTheme.isPlaying) return;
        PlayMusic(spaceTheme, GetClip(SPACE));
    }
}