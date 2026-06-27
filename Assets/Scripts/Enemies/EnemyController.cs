using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{

    [SerializeField] Instrument instrument;
    [SerializeField] ParticleSystem startPlayEffect;
    [SerializeField] ParticleSystem whilePlayEffect;
    [SerializeField] Material highlightMaterial;
    [SerializeField] int points;
    public UnityEvent onPlayInstrument;
    public bool IsPlaying { get; private set; }
    Material baseMaterial;
    SpriteRenderer renderer;
    bool isWalking;
    AudioSource audioSource;

    private void Awake() {
        renderer = GetComponentInChildren<SpriteRenderer>();
        baseMaterial = renderer.material;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() {
        audioSource.Play();

        onPlayInstrument.AddListener(AddScore);
        onPlayInstrument.AddListener(UpdateTrackSystem);
        onPlayInstrument.AddListener(TransitionState);
        onPlayInstrument.AddListener(TransitionSound);
        onPlayInstrument.AddListener(PlayEffects);

        GameManager.Instance.onLose.AddListener(StopSteps);
        GameManager.Instance.onLose.AddListener(StopPlayEffects);
    }

    private void AddScore()
    {
        ScoreManager.AddPoints?.Invoke(points);
    }

    private void UpdateTrackSystem()
    {
        TracksSystem.playInstrument?.Invoke(instrument.type, 1f);
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

    private void StopPlayEffects()
    {
        whilePlayEffect.Stop();
    }

    public void Trip()
    {
        GameManager.Instance.onLose?.Invoke();
        GameManager.Instance.onGameOver?.Invoke();
        StopSteps();
        audioSource.PlayOneShot(instrument.tripSound);
    }

    public void StopSteps()
    {
        if(isWalking)
        {
            GetComponent<MoveToPlayer>().enabled = false;
            audioSource.Stop();
            isWalking = false;
        }
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
