using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    public static AudioSourceManager Instance { get; private set; }

    [Serializable]
    private struct NamedAudioClip
    {
        public string name;
        public AudioClip clip;
    }

    private struct AudioSourcePoolEntry
    {
        public AudioSource source;
        public bool playing;
        public long timeStartedPlaying;
        public int poolIndex;
    }

    [SerializeField]
    private NamedAudioClip[] namedAudioClips;
    [SerializeField]
    private int audioSourcePoolSize = 0;

    private AudioSourcePoolEntry[] audioSourcePool;
    private Dictionary<string, AudioClip> stringClipMap;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InitializeAudioSourcePool();
        PopulateStringClipMap();
    }

    private void PopulateStringClipMap()
    {
        stringClipMap = new Dictionary<string, AudioClip>();

        foreach (var namedAudioClip in namedAudioClips)
        {
            stringClipMap.Add(namedAudioClip.name, namedAudioClip.clip);
        }
    }

    private void InitializeAudioSourcePool()
    {
        audioSourcePool = new AudioSourcePoolEntry[audioSourcePoolSize];

        for (int i = 0; i < audioSourcePoolSize; i++)
        {
            audioSourcePool[i] = new AudioSourcePoolEntry
            {
                source = gameObject.AddComponent<AudioSource>(),
                playing = false,
                timeStartedPlaying = 0,
                poolIndex = i
            };
        }
    }

    private AudioSourcePoolEntry GetLRUPoolEntry()
    {
        long minTimeStartedPlaying = audioSourcePool[0].timeStartedPlaying;
        AudioSourcePoolEntry leastRecentlyUsed = audioSourcePool[0];

        for (int i = 1; i < audioSourcePoolSize; i++)
        {
            var poolEntry = audioSourcePool[i];
            if (!poolEntry.playing)
            {
                return poolEntry;
            }

            if (poolEntry.timeStartedPlaying < minTimeStartedPlaying)
            {
                minTimeStartedPlaying = poolEntry.timeStartedPlaying;
                leastRecentlyUsed = poolEntry;
            }
        }

        return leastRecentlyUsed;
    }

    public void PlayClip(string clipName)
    {
        if (!stringClipMap.TryGetValue(clipName, out var audioClip))
        {
            Debug.Log($"Clip named '{clipName}' does not exist.");
            return;
        }

        var poolEntry = GetLRUPoolEntry();
        poolEntry.timeStartedPlaying = DateTime.Now.Ticks;
        poolEntry.playing = true;
        poolEntry.source.clip = audioClip;
        audioSourcePool[poolEntry.poolIndex] = poolEntry;

        poolEntry.source.Play();
        StartCoroutine(StopPlayingAfterClipEnds(poolEntry, audioClip));
    }

    private IEnumerator<WaitForSeconds> StopPlayingAfterClipEnds(AudioSourcePoolEntry poolEntry, AudioClip audioClip)
    {
        float clipLengthSeconds = audioClip.length;
        yield return new WaitForSeconds(clipLengthSeconds);

        poolEntry.playing = false;
        audioSourcePool[poolEntry.poolIndex] = poolEntry;
    }
}
