using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public delegate void SetPoints(int delta);
    public static SetPoints AddPoints;

    int points;
    public UnityEvent<int> onUpdatePoints;

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
    }

    void OnAddPoints(int delta)
    {
        points += delta;
        onUpdatePoints?.Invoke(points);
    }

    public int GetPoints()
    {
        return points;
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
