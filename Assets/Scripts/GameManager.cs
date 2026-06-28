using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-10)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UnityEvent onGameOver;
    public UnityEvent onLose;
    public UnityEvent onWin;
    public UnityEvent onStart;
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        onLose.AddListener(gameOver);
        onWin.AddListener(gameOver);
        onStart.AddListener(OnStart);
    }

    private void gameOver()
    {
        IsGameOver = true;
        onGameOver?.Invoke();
    }

    private void OnStart()
    {
        IsGameOver = false;
    }
}
