using System;
using System.Collections;
using UnityEngine;

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


    public static Action<Instruments, float>  enemyInstrument;
    public static Action<Instruments> priorizeInstrument;
    public static Action resetInstrument;
    public static Action stopMusic;

    private void Awake() {
        enemyInstrument = OnEnemyInstrument;
        priorizeInstrument = OnPriorizeInstrument;
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
    }

    void OnEnemyInstrument(Instruments instruments, float duration) {
        StartCoroutine(corrutine());

        IEnumerator corrutine() {
            OnPriorizeInstrument(instruments);

            yield return new WaitForSeconds(1 + duration);
            OnResetInstruments();
        }
    }

    void OnPriorizeInstrument(Instruments instruments) {
        StartCoroutine(corrutine());

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
        }
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
