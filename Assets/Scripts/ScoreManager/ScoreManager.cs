using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public delegate void SetPoints(int delta);
    public static SetPoints AddPoints;

    int points;
    [SerializeField] TMP_Text PointText;

    int highScore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        AddPoints = OnAddPoints;
        UpdateUI();
    }

    void OnAddPoints(int delta)
    {
        points += delta;
        UpdateUI();
    }

    void UpdateUI()
    {
        PointText.text = points.ToString();
    }

    void LoadHighScore()
    {
        if (PlayerPrefs.HasKey(Constants.HIGHSCORE))
        {
            highScore = PlayerPrefs.GetInt(Constants.HIGHSCORE);
        }
    }

    void SaveHighScore()
    {
        PlayerPrefs.SetInt(Constants.HIGHSCORE, highScore);
    }
}
