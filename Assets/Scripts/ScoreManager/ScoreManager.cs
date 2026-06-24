using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public delegate void SetPoints(int delta);
    public static SetPoints AddPoints;

    int points;
    [SerializeField] TMP_Text PointText;

    private void Awake() {
        AddPoints = OnAddPoints;
        UpdateUI();
    }

    void OnAddPoints(int delta) {
        points += delta;
        UpdateUI();
    }

    void UpdateUI() {
        PointText.text = points.ToString();
    }
}
