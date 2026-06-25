using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] TracksSystem.Instruments instrumentType;
    SpriteRenderer renderer;

    private void Awake() {
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable() {
        TracksSystem.priorizeInstrument?.Invoke(instrumentType);
    }

    public void OnPlayerClicksOn() {
        Debug.Log("ENEMY CLICKED!");
        TracksSystem.resetInstrument?.Invoke();
    }

    public SpriteRenderer getRenderer() { return renderer; }
}
