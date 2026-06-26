using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{

    [SerializeField] Instrument instrument;
    [SerializeField] ParticleSystem startPlayEffect;
    [SerializeField] ParticleSystem whilePlayEffect;
    public UnityEvent onPlayInstrument;
    SpriteRenderer renderer;
    AudioSource audioSource;

    private void Awake() {
        renderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() {
        audioSource.Play();

        onPlayInstrument.AddListener(UpdateTrackSystem);
        onPlayInstrument.AddListener(TransitionAnimation);
        onPlayInstrument.AddListener(TransitionSound);
        onPlayInstrument.AddListener(PlayEffects);
    }

    private void UpdateTrackSystem()
    {
        TracksSystem.priorizeInstrument?.Invoke(instrument.type);
    }

    private void TransitionAnimation()
    {
    }

    private void TransitionSound()
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.PlayOneShot(instrument.introSound);
    }

    private void PlayEffects()
    {
        startPlayEffect.Play();
        whilePlayEffect.Play();
    }

    public void OnPlayerClicksOn() {
        Debug.Log("ENEMY CLICKED!");
        TracksSystem.resetInstrument?.Invoke();
    }

    public SpriteRenderer getRenderer() { return renderer; }
}
