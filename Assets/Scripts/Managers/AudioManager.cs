using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonManager<AudioManager>, IManager {
    public ManagerStatus status {
        get { return managerStatus; }
    }

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
        EventManager.StartListener("templeMusic", TempleMusic);
        EventManager.StartListener("jungleMusic", JungleMusic);
        EventManager.StartListener("spaceMusic", SpaceMusic);
        PlayMusic(mainTheme, GetClip("MainTheme"));
        mainTheme.Play();
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

    private void TempleMusic() {
        PlayMusic(templeTheme, GetClip("TempleTheme"));
    }

    private void JungleMusic() {
        PlayMusic(jungleTheme, GetClip("JungleTheme"));
    }

    private void SpaceMusic() {
        PlayMusic(spaceTheme, GetClip("SpaceTheme"));
    }
}