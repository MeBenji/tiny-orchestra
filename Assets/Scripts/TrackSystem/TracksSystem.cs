using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksSystem : MonoBehaviour
{
    public enum Instruments
    {
        Bass,
        Celli,
        Cymbals,
        Flute,
        Harp,
        Snare,
        Trambone,
        Violin1,
        Violin2,
        Violin3
    }

    [System.Serializable]
    public struct InstrumentState
    {
        public Instruments instrument;
        public AudioSource track;
        public float volume;
    }

    [SerializeField] private float volumeFadeTime = 1f;
    private Coroutine fadeCoroutine;
    private double playDelay = 4.0;

    [SerializeField] List<InstrumentState> instrumentStates;
    Dictionary<Instruments, InstrumentState> instrumentStates_dict;

    public float maxVolume = 1f;

    public static Action<Instruments, float> playInstrument;

    private void Awake()
    {
        playInstrument = OnPlayInstrument;

        instrumentStates_dict = new Dictionary<Instruments, InstrumentState>();
        foreach(InstrumentState state in instrumentStates)
        {
            state.track.Stop();
            state.track.volume = state.volume;
            instrumentStates_dict.Add(state.instrument, state);
        }

        PlayMusic();
    }

    private void OnEnable()
    {
        GameManager.Instance.onLose.AddListener(StopMusic);
    }

    private void OnDisable()
    {
        GameManager.Instance.onLose.RemoveListener(StopMusic);
    }

    void OnPlayInstrument(Instruments instrument, float volume)
    {
        InstrumentState state = instrumentStates_dict[instrument];

        if(state.volume < maxVolume)
        {
            float targetVolume = Mathf.Clamp(state.volume + volume, 0f, maxVolume);
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeVolume(state.track, state.volume, targetVolume));
            state.volume = targetVolume;
        }
    }

    IEnumerator FadeVolume(AudioSource track, float start, float target)
    {
        float t = 0;
        while (t < volumeFadeTime)
        {
            t += Time.deltaTime;
            Mathf.Clamp01(t);
            track.volume = Mathf.Lerp(start, target, t);
            yield return null;
        }
    }

    void PlayMusic()
    {
        double startDSPTime = AudioSettings.dspTime + playDelay;
        foreach (InstrumentState state in instrumentStates)
        {
            state.track.PlayScheduled(startDSPTime);
        }
    }

    void StopMusic()
    {
        foreach (InstrumentState state in instrumentStates)
        {
            StartCoroutine(FadeVolume(state.track, state.volume, 0f));
        }
    }
}
