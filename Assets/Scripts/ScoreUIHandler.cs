using TMPro;
using UnityEngine;

public class ScoreUIHandler : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Start()
    {
        int points = ScoreManager.Instance.GetPoints();
        UpdateUI(points);
    }

    private void OnEnable()
    {
        ScoreManager.Instance.onUpdatePoints.AddListener(UpdateUI);
    }

    private void OnDisable()
    {
        ScoreManager.Instance.onUpdatePoints.RemoveListener(UpdateUI);
    }

    private void UpdateUI(int points)
    {
        text.text = points.ToString();
    }
}
