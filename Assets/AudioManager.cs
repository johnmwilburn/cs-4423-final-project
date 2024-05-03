using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    Dictionary<string, int> clipSourceIndices = new Dictionary<string, int>()
    {
        {"music", 0},
        {"shoot", 1},
        {"hit", 2},
        {"rat_death", 3},
        {"pickup", 4}
    };

    List<AudioSource> audioSources;

    void Start()
    {
        audioSources = new List<AudioSource>(GetComponents<AudioSource>());
    }

    public void Play(string clip_name)
    {
        int sourceIndex;
        clipSourceIndices.TryGetValue(clip_name, out sourceIndex);
        AudioSource audio_source = audioSources[sourceIndex];
        audio_source.Play();
    }
}
