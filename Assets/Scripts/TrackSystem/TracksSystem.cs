using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static TracksSystem;

public class TracksSystem : MonoBehaviour
{
    public enum Instruments {
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

    [SerializeField] AudioSource[] tracks;
    [SerializeField] float maxVol = 1.2f;
    [SerializeField] float midVol = 1;
    [SerializeField] float minVol = 0;

    List<Instruments> focusedInstruments = new();
    public static Action<Instruments, float>  enemyInstrument;
    public static Action<Instruments> priorizeInstrument;
    public static Action<Instruments> unpriorizeInstrument;
    public static Action resetInstrument;
    public static Action stopMusic;

    private void Awake() {
        enemyInstrument = OnEnemyInstrument;
        priorizeInstrument = OnPriorizeInstrument;
        unpriorizeInstrument = OnResetInstrument;
        resetInstrument = OnResetInstruments;
        stopMusic = OnStopMusic;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)){
            OnEnemyInstrument((Instruments)(0), 2);
        }

        if(Input.GetKeyDown(KeyCode.Y)) {
            OnEnemyInstrument((Instruments)(1), 2);
        }

        UpdateTracksVolume();
    }

    void UpdateTracksVolume() {
        for(int i = 0; i < tracks.Length; i++) {
            bool listEmpity = focusedInstruments.Count > 0;
            float target = listEmpity ? focusedInstruments.Contains((Instruments)i) ? maxVol : minVol : midVol;

            float speed = listEmpity ? 30 : 10;

            tracks[i].volume = Mathf.MoveTowards(tracks[i].volume, target, Time.deltaTime * speed);
        }
    }

    void OnEnemyInstrument(Instruments instruments, float duration) {
        StartCoroutine(corrutine());

        IEnumerator corrutine() {
            OnPriorizeInstrument(instruments);

            yield return new WaitForSeconds(1 + duration);
            OnResetInstrument(instruments);
        }
    }

    void OnPriorizeInstrument(Instruments instruments) {

        focusedInstruments.Add(instruments);

        /*StartCoroutine(corrutine());

        IEnumerator corrutine() {
            float t = 0;
            while(t <= 1) {
                for(int i = 0; i < tracks.Length; i++) {
                    float target = i == (int)(instruments) ? maxVol : minVol;

                    tracks[i].volume = Mathf.Lerp(tracks[i].volume, target, t);
                }

                t += Time.deltaTime;
                yield return null;
            }
        }*/
    }

    void OnResetInstrument(Instruments instruments) {
        focusedInstruments.Remove(instruments);
    }


    void OnResetInstruments() {
        StartCoroutine(corrutine());

        IEnumerator corrutine() {
            float t = 0;
            while(t <= 1) {
                for(int i = 0; i < tracks.Length; i++) {
                    tracks[i].volume = Mathf.Lerp(tracks[i].volume, midVol, t);
                }

                t += Time.deltaTime;
                yield return null;
            }
        }
    }

    void OnStopMusic()
    {
        foreach(AudioSource track in tracks)
        {
            track.Stop();
        }
    }
}
