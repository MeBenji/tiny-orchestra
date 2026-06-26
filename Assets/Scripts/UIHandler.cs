using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Start()
    {
        ScoreManager.Instance.onUpdatePoints.AddListener(UpdateUI);
        int points = ScoreManager.Instance.GetPoints();
        UpdateUI(points);
    }

    private void UpdateUI(int points)
    {
        text.text = points.ToString();
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.onUpdatePoints.RemoveListener(UpdateUI);
    }
}
