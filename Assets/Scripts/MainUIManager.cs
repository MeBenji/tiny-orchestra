using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] GameObject victoryUI;
    [SerializeField] GameObject defeatUI;
    
    void OnEnable()
    {
        GameManager.Instance.onWin.AddListener(ShowVictoryUI);
        GameManager.Instance.onLose.AddListener(ShowDefeatUI);
    }

    private void OnDisable()
    {
        GameManager.Instance.onWin.RemoveListener(ShowVictoryUI);
        GameManager.Instance.onLose.RemoveListener(ShowDefeatUI);
    }

    void ShowVictoryUI()
    {
        victoryUI.SetActive(true);
    }

    void ShowDefeatUI()
    {
        defeatUI.SetActive(true);
    }
}
