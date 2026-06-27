using UnityEngine;

[CreateAssetMenu(menuName = "Tiny Orchestra/Instrument")]
public class Instrument : ScriptableObject
{
    public TracksSystem.Instruments type;
    public AudioClip track;
    public AudioClip introSound;
    public AudioClip tripSound;
}
