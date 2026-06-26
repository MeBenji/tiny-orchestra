using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{

    [SerializeField] Instrument instrument;
    [SerializeField] ParticleSystem startPlayEffect;
    [SerializeField] ParticleSystem whilePlayEffect;
    [SerializeField] Material highlightMaterial;
    public UnityEvent onPlayInstrument;
    public bool IsPlaying { get; private set; }
    Material baseMaterial;
    SpriteRenderer renderer;
    AudioSource audioSource;

    private void Awake() {
        renderer = GetComponentInChildren<SpriteRenderer>();
        baseMaterial = renderer.material;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() {
        audioSource.Play();

        onPlayInstrument.AddListener(UpdateTrackSystem);
        onPlayInstrument.AddListener(TransitionState);
        onPlayInstrument.AddListener(TransitionSound);
        onPlayInstrument.AddListener(PlayEffects);
    }

    private void UpdateTrackSystem()
    {
        TracksSystem.priorizeInstrument?.Invoke(instrument.type);
    }

    private void TransitionState()
    {
        IsPlaying = true;
        GetComponent<BoxCollider>().enabled = false;
        Select(false);
        GetComponent<MoveToPlayer>().enabled = false;
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

    public void Select(bool isSelected)
    {
        if (IsPlaying) {
            renderer.material = baseMaterial;
            return;
        }
        renderer.material = isSelected ? highlightMaterial : baseMaterial;
    }
}
